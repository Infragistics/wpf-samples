using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using IGUndoRedoFramework.Resources;
using Infragistics;
using Infragistics.Controls.Schedules;
using Infragistics.Undo;

namespace IGUndoRedoFramework.UndoUnits
{
    #region ScheduleDataManagerUndoHelper
    /// <summary>
    /// Helper class for adding undo operations to an <see cref="UndoManager"/> for a <see cref="XamScheduleManager"/>
    /// </summary>
    public class ScheduleDataManagerUndoHelper
    {
        /// <summary>
        /// Adds an undo operation for the ActivityBase add operation.
        /// </summary>
        /// <param name="undoManager">The UndoManager with which to register the operation</param>
        /// <param name="dataManager">The XamScheduleDataManager for which the activity was added</param>
        /// <param name="activityAdded">The activity instance that was added</param>
        public static void AddActivityAddChange(UndoManager undoManager, XamScheduleDataManager dataManager, ActivityBase activityAdded)
        {
            if (null != undoManager)
                undoManager.AddChange(new ActivityAddedUndoUnit(dataManager, activityAdded));
        }

        /// <summary>
        /// Adds an undo operation for the ActivityBase change/edit operation.
        /// </summary>
        /// <param name="undoManager">The UndoManager with which to register the operation</param>
        /// <param name="dataManager">The XamScheduleDataManager for which the activity was removed</param>
        /// <param name="activity">The activity instance that was modified</param>
        /// <param name="originalActivityData">The activity instance representing the original state of the activity prior to the beginning of the edit</param>
        public static void AddActivityModifiedChanged(UndoManager undoManager, XamScheduleDataManager dataManager, ActivityBase activity, ActivityBase originalActivityData)
        {
            if (null == undoManager)
                return;

            var bag = ActivityPropertyBag.Create(activity, originalActivityData);

            if (bag != null)
            {
                // if the recurrence version changes or it was a recurrence and no longer is then 
                // the variances are lost so we should remove any change units for the occurrences
                if ((activity.IsRecurrenceRoot && activity.RecurrenceVersion != originalActivityData.RecurrenceVersion)
                    ||
                    (originalActivityData.IsRecurrenceRoot && !activity.IsRecurrenceRoot)
                    )
                {
                    ScheduleUndoUnit.RemoveAllOccurrenceUnits(undoManager, activity);
                }

                undoManager.AddChange(new ActivityChangedUndoUnit(dataManager, activity, bag));
            }
        }

        /// <summary>
        /// Adds an undo operation for the ActivityBase removal operation.
        /// </summary>
        /// <param name="undoManager">The UndoManager with which to register the operation</param>
        /// <param name="dataManager">The XamScheduleDataManager for which the activity was removed</param>
        /// <param name="activityRemoved">The activity instance that was removed</param>
        public static void AddActivityRemoveChange(UndoManager undoManager, XamScheduleDataManager dataManager, ActivityBase activityRemoved)
        {
            if (null != undoManager)
                undoManager.AddChange(new ActivityRemovedUndoUnit(dataManager, activityRemoved));
        }
    }
    #endregion //ScheduleDataManagerUndoHelper

    #region ActivityPropertyBag
    public class ActivityPropertyBag
    {
        #region Member Variables

        private PropertyBag _propertyBag;

        #endregion //Member Variables

        #region Constructor
        private ActivityPropertyBag(PropertyBag propertyBag)
        {
            _propertyBag = propertyBag ?? new PropertyBag();
        }
        #endregion //Constructor

        #region Methods

        #region Public Methods

        #region Apply
        public void Apply(ActivityBase activity)
        {
            if (activity == null)
                throw new ArgumentNullException("activity");

            foreach (var item in _propertyBag)
            {
                var propInfo = item.Key as PropertyInfo;
                var value = item.Value;

                if (null != propInfo)
                    ApplyValue(activity, propInfo, value);
                else if (item.Key is string)
                    activity.Metadata[item.Key as string] = value;
                else
                {
                    Debug.Assert(false, "Unexpected key");
                }
            }
        }
        #endregion //Apply

        #region Create(ActivityBase)
        public static ActivityPropertyBag Create(ActivityBase activity)
        {
            if (activity == null)
                throw new ArgumentNullException("activity");

            var bag = new PropertyBag();
            var props = GetProperties(activity.GetType());

            foreach (var prop in props)
            {
                // note this doesn't deep clone objects like the 
                // recurrence, reminder, etc. but for the purposes of 
                // caching the values when removing an activity 
                // that shouldn't be needed
                bag.Add(prop, prop.GetValue(activity, null));
            }

            foreach (var metadataItem in activity.Metadata)
            {
                bag.Add(metadataItem.Key, metadataItem.Value);
            }

            return new ActivityPropertyBag(bag);
        }
        #endregion //Create(ActivityBase)

        #region Create(ActivityBase, ActivityBase)
        public static ActivityPropertyBag Create(ActivityBase original, ActivityBase modified)
        {
            if (original == null)
                throw new ArgumentNullException("original");

            if (modified == null)
                throw new ArgumentNullException("modified");

            if (original.GetType() != modified.GetType())
                throw new ArgumentException();

            var values = CopyValues(original, modified);

            Debug.Assert(values == null || values is PropertyBag, "Expected bag or properties");

            var bag = values as PropertyBag ?? new PropertyBag();

            foreach (var metadataItem in original.Metadata)
            {
                var originalValue = metadataItem.Value;
                var newValue = modified.Metadata[metadataItem.Key];

                if (object.Equals(originalValue, newValue))
                    continue;

                bag.Add(metadataItem.Key, newValue);
            }

            if (bag.Count == 0)
                return null;

            return new ActivityPropertyBag(bag);
        }
        #endregion //Create(ActivityBase, ActivityBase)

        #region GetDescription
        public string GetDescription(ActivityBase activity)
        {
            if (_propertyBag.Count == 1)
            {
                var first = _propertyBag.First();

                string name;

                if (first.Key is PropertyInfo)
                    name = ((PropertyInfo)first.Key).Name;
                else if (first.Key is string)
                {
                    // metadata
                    name = (string)first.Key;
                }
                else
                {
                    Debug.Assert(false, "Unexpected key");
                    return null;
                }

                string activityTypeLocalized = CommonHelper.GetLocalizedActivity(activity.ActivityType);

                return string.Format(UndoRedoStrings.UndoRedo_Change01, activityTypeLocalized, name);
            }

            bool hasMoved, hasOther;
            hasMoved = hasOther = false;

            foreach (var pair in _propertyBag)
            {
                PropertyInfo pi = pair.Key as PropertyInfo;

                if (pi == null)
                    break;

                switch (pi.Name)
                {
                    case "Start":
                    case "End":
                    case "OwningResourceId":
                    case "OwningResource":
                    case "OwningCalendarId":
                    case "OwningCalendar":
                        hasMoved = true;
                        break;
                    default:
                        hasOther = true;
                        break;
                }

                if (hasOther)
                    break;
            }

            if (!hasOther)
            {
                if (hasMoved)
                    return string.Format(UndoRedoStrings.UndoRedo_Move, CommonHelper.GetLocalizedActivity(activity.ActivityType));
            }

            return string.Format(UndoRedoStrings.UndoRedo_Change, CommonHelper.GetLocalizedActivity(activity.ActivityType));
        }
        #endregion //GetDescription

        #endregion //Public Methods

        #region Private Methods

        #region ApplyValue
        private static void ApplyValue(object target, PropertyInfo property, object value)
        {
            var propBag = value as Tuple<object, PropertyBag>;

            if (propBag == null)
                property.SetValue(target, value, null);
            else
            {
                Debug.Assert(object.Equals(propBag.Item1, property.GetValue(target, null)), "The reference is now different?");

                foreach (var item in propBag.Item2)
                {
                    Debug.Assert(item.Key is PropertyInfo, "Expected property info's only in the bag");
                    ApplyValue(value, item.Key as PropertyInfo, item.Value);
                }
            }
        }
        #endregion //ApplyValue

        #region AreValuesEqual
        private static bool AreValuesEqual(object o1, object o2)
        {
            if (object.ReferenceEquals(o1, o2))
                return true;

            if (o1 == null)
                return o2 == null;
            else if (o2 == null)
                return false;

            var oType = o1.GetType();

            // if the values are different types then they're different
            if (oType != o2.GetType())
                return true;

            // if this is a value type then we can just use equality checks
            if (oType.IsValueType || o1 is string)
                return object.Equals(o1, o2);

            var props = GetProperties(o1.GetType());

            // if there are no public get/set properties to compare...
            if (props.Count == 0)
                return object.Equals(o1, o2);

            foreach (var prop in props)
            {
                var o1Value = prop.GetValue(o1, null);
                var o2Value = prop.GetValue(o2, null);

                if (!AreValuesEqual(o1Value, o2Value))
                    return false;
            }

            return true;
        }
        #endregion //AreValuesEqual

        #region CopyValues
        private static object CopyValues(object original, object modified)
        {
            // if there was no value then the new modified value can be stored
            if (original == null)
                return modified;

            // if the value was nulled out then we can return null
            if (modified == null)
                return null;

            PropertyBag bag = null;
            var props = GetProperties(original.GetType());

            foreach (var prop in props)
            {
                if (!ShouldIncludeProperty(prop))
                    continue;

                var originalValue = prop.GetValue(original, null);
                var newValue = prop.GetValue(modified, null);

                if (AreValuesEqual(originalValue, newValue))
                {
                    // the properties may be the same but could be complex
                    // reference types with sub properties that need to be 
                    // cloned
                    if (originalValue != null && !prop.PropertyType.IsValueType)
                    {
                        var value = CopyValues(originalValue, newValue);

                        if (value != null)
                        {
                            PropertyBag innerBag = value as PropertyBag;

                            if (null != innerBag)
                                bag.Add(prop, new Tuple<object, PropertyBag>(newValue, innerBag));
                            else
                                bag.Add(prop, value);
                        }
                    }

                    continue;
                }

                if (bag == null)
                    bag = new PropertyBag();

                bag.Add(prop, newValue);
            }

            return bag;
        }
        #endregion //CopyValues

        #region GetProperties
        private static IList<PropertyInfo> GetProperties(Type objectType)
        {
            var list = new List<PropertyInfo>();

            foreach (var info in objectType.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy))
            {
                // skip the ones that wouldn't be serialized like the owningresource and owningcalendar. the id's will 
                // handle these and we won't have to worry about if the actual resource/calendar object changes
                var attribs = info.GetCustomAttributes(typeof(System.Xml.Serialization.XmlIgnoreAttribute), true);

                if (attribs != null && attribs.Length > 0)
                    continue;

                if (info.CanRead && info.CanWrite)
                    list.Add(info);
            }

            return list;
        }
        #endregion //GetProperties

        #region ShouldIncludeProperty
        private static bool ShouldIncludeProperty(PropertyInfo property)
        {
            switch (property.Name)
            {
                case "IsVariance":
                    {
                        // there is no way to make a variance not be a variance
                        // however the variant flags should get reset so at least 
                        // the properties will still synchronize with changes to 
                        // the root
                        if (property.DeclaringType == typeof(ActivityBase))
                            return false;

                        break;
                    }
                case "ReminderEnabled":
                case "ReminderInterval":
                case "Reminder":
                    {
                        // ignore the reminder properties
                        if (property.DeclaringType == typeof(ActivityBase))
                            return false;

                        break;
                    }
            }

            return true;
        }
        #endregion //ShouldIncludeProperty

        #endregion //Private Methods

        #endregion //Methods

        #region PropertyBag class
        // use a custom object so we can identify the value as our object
        private class PropertyBag : Dictionary<object, object>
        {
        }
        #endregion //PropertyBag class
    }
    #endregion //ActivityPropertyBag

    #region ScheduleUndoUnit
    /// <summary>
    /// Base class for an <see cref="UndoUnit"/> that affects the <see cref="XamScheduleDataManager"/>
    /// </summary>
    public abstract class ScheduleUndoUnit : UndoUnit
    {
        #region Member Variables

        private XamScheduleDataManager _dataManager;

        #endregion //Member Variables

        #region Constructor
        /// <summary>
        /// Initializes a new <see cref="ScheduleUndoUnit"/>
        /// </summary>
        /// <param name="dataManager">The associated XamScheduleDataManager that was changed</param>
        protected ScheduleUndoUnit(XamScheduleDataManager dataManager)
        {
            if (dataManager == null)
                throw new ArgumentNullException("dataManager");

            _dataManager = dataManager;
        }
        #endregion //Constructor

        #region Base class overrides
        /// <summary>
        /// Returns the associated <see cref="XamScheduleDataManager"/>
        /// </summary>
        public override object Target
        {
            get { return this.DataManager; }
        }
        #endregion //Base class overrides

        #region Properties
        /// <summary>
        /// Returns the <see cref="XamScheduleDataManager"/> to be affected by the change.
        /// </summary>
        public XamScheduleDataManager DataManager
        {
            get { return _dataManager; }
        }
        #endregion //Properties

        #region Methods

        #region RemoveAllOccurrenceUnits
        internal static void RemoveAllOccurrenceUnits(UndoManager undoManager, ActivityBase recurrenceRoot)
        {
            if (null == undoManager)
                throw new ArgumentNullException("undoManager");

            if (recurrenceRoot == null)
                throw new ArgumentNullException("recurrenceRoot");

            // when deleting a series we need to remove any undo/redo operations for the variances
            undoManager.RemoveAll((u) =>
            {
                var activityUnit = u as ActivityUndoUnit;
                return activityUnit != null && activityUnit.Activity.RootActivity == recurrenceRoot;
            });
        }
        #endregion //RemoveAllOccurrenceUnits

        #endregion //Methods
    }
    #endregion //ScheduleUndoUnit

    #region ActivityUndoUnit
    /// <summary>
    /// Base class for an <see cref="UndoUnit"/> relating to a single <see cref="ActivityBase"/> that affects a <see cref="XamScheduleDataManager"/>
    /// </summary>
    public abstract class ActivityUndoUnit : ScheduleUndoUnit
    {
        #region Constructor
        /// <summary>
        /// Initializes a new <see cref="ActivityUndoUnit"/>
        /// </summary>
        /// <param name="dataManager">The associated XamScheduleDataManager that was changed</param>
        protected ActivityUndoUnit(XamScheduleDataManager dataManager)
            : base(dataManager)
        {
        }
        #endregion //Constructor

        #region Properties

        /// <summary>
        /// Returns the associated <see cref="Activity"/>
        /// </summary>
        public abstract ActivityBase Activity { get; }

        #endregion //Properties
    }
    #endregion //ActivityUndoUnit

    #region ActivityAddedUndoUnit
    /// <summary>
    /// A custom XamSchedule <see cref="UndoUnit"/> used when a new <see cref="ActivityBase"/> has created.
    /// </summary>
    public class ActivityAddedUndoUnit : ActivityUndoUnit
    {
        #region Member Variables

        private ActivityBase _activityAdded;

        #endregion //Member Variables

        #region Constructor
        /// <summary>
        /// Initializes a new <see cref="ActivityAddedUndoUnit"/>
        /// </summary>
        /// <param name="dataManager">The associated XamScheduleDataManager that was changed</param>
        /// <param name="activityAdded">The activity that was added</param>
        public ActivityAddedUndoUnit(XamScheduleDataManager dataManager, ActivityBase activityAdded)
            : base(dataManager)
        {
            if (activityAdded == null)
                throw new ArgumentNullException("activityAdded");

            _activityAdded = activityAdded;
        }
        #endregion //Constructor

        #region Base class overrides

        #region Activity
        /// <summary>
        /// Returns the associated <see cref="Activity"/>
        /// </summary>
        public override ActivityBase Activity
        {
            get { return _activityAdded; }
        }
        #endregion //Activity

        #region Execute
        /// <summary>
        /// Removes the activity that was added.
        /// </summary>
        /// <param name="executeInfo">Provides information about the operation</param>
        /// <returns>True if the added activity was removed; otherwise false if it was cancelled</returns>
        protected override bool Execute(UndoExecuteContext executeInfo)
        {
            var result = this.DataManager.Remove(_activityAdded);

            Debug.Assert(result.IsComplete, "TODO: Operation is not complete. It is possible the operation will not succeed.");

            if (!result.IsCanceled && _activityAdded.IsRecurrenceRoot)
            {
                RemoveAllOccurrenceUnits(executeInfo.UndoManager, _activityAdded);
            }

            return !result.IsCanceled;
        }
        #endregion //Execute

        #region GetDescription
        /// <summary>
        /// Returns a description of the add operation.
        /// </summary>
        /// <param name="itemType">The type of history for which the description is being requested</param>
        /// <param name="detailed">True to obtain a detailed description</param>
        /// <returns>A string description of the operation</returns>
        public override string GetDescription(UndoHistoryItemType itemType, bool detailed)
        {
            // this could be the result of an undo of a remove
            string prefix = itemType == UndoHistoryItemType.Redo ? UndoRedoStrings.UndoRedo_Remove : UndoRedoStrings.UndoRedo_Add;

            return string.Format(prefix, CommonHelper.GetLocalizedActivity(_activityAdded.ActivityType));
        }
        #endregion //GetDescription

        #endregion //Base class overrides
    }
    #endregion //ActivityAddedUndoUnit

    #region ActivityRemovedUndoUnit
    /// <summary>
    /// A custom XamSchedule <see cref="UndoUnit"/> used when an <see cref="ActivityBase"/> has been removed from the control.
    /// </summary>
    public class ActivityRemovedUndoUnit : ActivityUndoUnit
    {
        #region Member Variables

        private ActivityPropertyBag _oldValues;
        private ActivityBase _activityRemoved;

        #endregion //Member Variables

        #region Constructor
        /// <summary>
        /// Initializes a new <see cref="ActivityRemovedUndoUnit"/>
        /// </summary>
        /// <param name="dataManager">The associated XamScheduleDataManager that was changed</param>
        /// <param name="activityRemoved">The activity that was removed</param>
        public ActivityRemovedUndoUnit(XamScheduleDataManager dataManager, ActivityBase activityRemoved)
            : base(dataManager)
        {
            if (activityRemoved == null)
                throw new ArgumentNullException("activityRemoved");

            _oldValues = ActivityPropertyBag.Create(activityRemoved);
            _activityRemoved = activityRemoved;
        }

        #endregion //Constructor

        #region Base class overrides

        #region Activity
        /// <summary>
        /// Returns the associated <see cref="Activity"/>
        /// </summary>
        public override ActivityBase Activity
        {
            get { return _activityRemoved; }
        }
        #endregion //Activity

        #region Execute
        /// <summary>
        /// Creates a new activity for the one that was removed.
        /// </summary>
        /// <param name="executeInfo">Provides information about the operation</param>
        /// <returns>True if the removed activity was recreated; otherwise false if it was cancelled</returns>
        protected override bool Execute(UndoExecuteContext executeInfo)
        {
            if (_activityRemoved.IsOccurrence)
            {
                // when an occurrence is deleted the isoccurrence deleted is set to 
                // true during the remove. so we'll clear that flag
                _activityRemoved.IsOccurrenceDeleted = false;

                // because that doesn't trigger one of the datamanager events
                // we need to add a history item to be able to redo the operation
                executeInfo.UndoManager.AddChange(new ActivityAddedUndoUnit(this.DataManager, _activityRemoved));

                return true;
            }

            DataErrorInfo error;
            var newActivity = this.DataManager.CreateNew(_activityRemoved.ActivityType, out error);

            if (newActivity == null || error != null)
                return false;

            if (!this.DataManager.BeginEdit(newActivity, out error) || error != null)
                return false;

            _oldValues.Apply(newActivity);
            var result = this.DataManager.EndEdit(newActivity, true);

            executeInfo.UndoManager.ForEach((u) =>
            {
                var changeUnit = u as ActivityChangedUndoUnit;

                // there could have been changes to the activity before it was deleted 
                // so we need to fix up their activity references
                if (changeUnit != null)
                    changeUnit.OnActivityUndeleted(_activityRemoved, newActivity);
            });

            Debug.Assert(result.IsComplete, "TODO: Operation is not complete. It is possible the operation will not succeed.");

            return !result.IsCanceled;
        }
        #endregion //Execute

        #region GetDescription
        /// <summary>
        /// Returns a description of the remove operation.
        /// </summary>
        /// <param name="itemType">The type of history for which the description is being requested</param>
        /// <param name="detailed">True to obtain a detailed description</param>
        /// <returns>A string description of the operation</returns>
        public override string GetDescription(UndoHistoryItemType itemType, bool detailed)
        {
            // this could be the result of an undo of a add
            string prefix = itemType == UndoHistoryItemType.Redo ? UndoRedoStrings.UndoRedo_Add : UndoRedoStrings.UndoRedo_Remove;
            
            return string.Format(prefix, CommonHelper.GetLocalizedActivity(_activityRemoved.ActivityType));
        }
        #endregion //GetDescription

        #endregion //Base class overrides
    }
    #endregion //ActivityRemovedUndoUnit

    #region ActivityChangedUndoUnit
    /// <summary>
    /// A custom XamSchedule <see cref="UndoUnit"/> used when the properties of an <see cref="ActivityBase"/> have been edited.
    /// </summary>
    public class ActivityChangedUndoUnit : ActivityUndoUnit
    {
        #region Member Variables

        private ActivityPropertyBag _propertyBag;
        private ActivityBase _activity;
        private string _description;

        #endregion //Member Variables

        #region Constructor
        /// <summary>
        /// Initializes a new <see cref="ActivityChangedUndoUnit"/>
        /// </summary>
        /// <param name="dataManager">The associated XamScheduleDataManager that was changed</param>
        /// <param name="activity">The activity that was modified</param>
        /// <param name="originalValues">The clone activity with the original values</param>
        public ActivityChangedUndoUnit(XamScheduleDataManager dataManager, ActivityBase activity, ActivityPropertyBag propertyBag)
            : base(dataManager)
        {
            if (activity == null)
                throw new ArgumentNullException("activity");

            if (propertyBag == null)
                throw new ArgumentNullException("propertyBag");

            _propertyBag = propertyBag;
            _activity = activity;

            _description = _propertyBag.GetDescription(activity);
        }

        #endregion //Constructor

        #region Base class overrides

        #region Activity
        /// <summary>
        /// Returns the associated <see cref="Activity"/>
        /// </summary>
        public override ActivityBase Activity
        {
            get { return _activity; }
        }
        #endregion //Activity

        #region Execute
        /// <summary>
        /// Creates a new activity for the one that was removed.
        /// </summary>
        /// <param name="executeInfo">Provides information about the operation</param>
        /// <returns>True if the removed activity was recreated; otherwise false if it was cancelled</returns>
        protected override bool Execute(UndoExecuteContext executeInfo)
        {
            DataErrorInfo error;

            if (!this.DataManager.BeginEdit(_activity, out error) || error != null)
                return false;

            _propertyBag.Apply(_activity);

            var result = this.DataManager.EndEdit(_activity, true);

            Debug.Assert(result.IsComplete, "TODO: Operation is not complete. It is possible the operation will not succeed.");

            return !result.IsCanceled;
        }
        #endregion //Execute

        #region GetDescription
        /// <summary>
        /// Returns a description of the change operation.
        /// </summary>
        /// <param name="itemType">The type of history for which the description is being requested</param>
        /// <param name="detailed">True to obtain a detailed description</param>
        /// <returns>A string description of the operation</returns>
        public override string GetDescription(UndoHistoryItemType itemType, bool detailed)
        {
            return _description;
        }
        #endregion //GetDescription

        #endregion //Base class overrides

        #region Methods

        #region OnActivityUndeleted
        internal void OnActivityUndeleted(ActivityBase originalActivity, ActivityBase newActivity)
        {
            if (originalActivity == _activity)
                _activity = newActivity;
        }
        #endregion //OnActivityUndeleted

        #endregion //Methods
    }
    #endregion //ActivityRemovedUndoUnit

    public class CommonHelper
    {
        public static string GetLocalizedActivity(ActivityType activityType)
        {
            string activityTypeLocalized = string.Empty;

            switch (activityType)
            {
                case ActivityType.Appointment:
                    activityTypeLocalized = UndoRedoStrings.UndoRedo_Schedule_Appointment;
                    break;
                case ActivityType.Journal:
                    activityTypeLocalized = UndoRedoStrings.UndoRedo_Schedule_Journal;
                    break;
                case ActivityType.Task:
                    activityTypeLocalized = UndoRedoStrings.UndoRedo_Schedule_Task;
                    break;
            }

            return activityTypeLocalized;
        }  
    }
}
