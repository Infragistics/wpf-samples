using System;
using System.Collections.Generic;
using System.Reflection;

namespace Infragistics.Framework
{
    public class SamplesGroup : List<SamplesModel>  
    {
        public string Name { get; set; }

        public SamplesGroup()
        { 
        }
        public SamplesGroup(string name)
        { 
            Name = name;
        }
         
        public override string ToString()
        {
            return this.Name + " " + this.Count(); 
        }
    }
     
    public class SampleResult : Result //<T> : Result<T>
    {
        public SampleResult()
        {

        }
        public SampleResult(Exception error)
            : base(error)
        {
        }
        //public static SampleResult<T> Fail(Exception error)
        //{
        //    return new SampleResult<T>(error);
        //}
        //public static SampleResult<T> Fail(string error)
        //{
        //    return new SampleResult<T>(new Exception(error));
        //}
        //public static SampleResult<T> Success(T v)
        //{
        //    return new SampleResult<T> { Value = v };
        //}

    }
    public class SamplesModel  
    {
        public SamplesModel()
        { 
        } 
        
        public int Index { get; set; }

        /// <summary> Gets or sets Type </summary>
        public Type Type { get; set; } 
         
        /// <summary> Gets or sets Name </summary>
        public string Name { get; set; }
        public string NameIndent { get; set; }
        public string Group { get; set; }
        public string Title { get; set; }
        
        /// <summary> Gets or sets Name </summary>
        public string Namespace { get; set; }

        /// <summary> Gets or sets Name </summary>
        public string TypeName { get { return this.Type.Name; } }

        public new string ToString()
        {
            //return this.Index.ToString("000") + " - " + this.Type.Name;
            return this.Group + " - " + this.Name;
            //return this.Index.ToString("000") + " - " + this.Name; 
        }
        public Result<T> CreateView<T>()
        {
            if (this.Type == null)
                Result<T>.Fail(this.Name + " sample type is not provided");
            
            try
            {
                object sampleInstance = null;
                var constructors = this.Type.GetTypeInfo().DeclaredConstructors;
                foreach (var costr in constructors)
                {
                    if (costr.GetParameters().Length == 0)
                    {
                        sampleInstance = costr.Invoke(null);
                        break;
                    }
                }
                var sampleView = (T)sampleInstance;
                return Result<T>.Success(sampleView);
            }
            catch (Exception ex)
            {
                return Result<T>.Fail(ex);  
            }  
        }
    }

}