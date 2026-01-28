
using System;

namespace Infragistics.Framework
{
    public class Result<T>
    {
        public Result()
        {
            Error = null;
        }
        public Result(Exception error, bool logAsError = true)
        {
            Error = error;
            if (logAsError)
                Logs.Error(error);
            else
                Logs.Warning(error.Message);
        }
        public Result(Exception error, string errorSource)
        {
            Error = error;
            ErrorSource = errorSource;
        }
        public Result(T value)
        {
            Error = null;
            Value = value;

            if (value is string && string.IsNullOrEmpty(value.ToString()))
            {
                Logs.Message(value.ToString());
            }
        }
        public T Value { get; set; }

        public Exception Error { get; private set; }
        public string ErrorSource { get; private set; }

        public string ErrorMessage
        {
            get { return Error == null ? "" : Error.ToString(true); }
        }

        public bool IsValid
        {
            get { return Error == null; }
        }
        public bool HasError
        {
            get { return Error != null; }
        }

        public static Result<T> Fail(Exception error)
        {
            return new Result<T>(error);
        }

        public static Result<T> Fail(Exception error, string errorSource)
        {
            return new Result<T>(error, errorSource);
        }
        public static Result<T> Fail(string errorMessage)
        {
            return new Result<T>(new Exception(errorMessage));
        }
        public static Result<T> Success(T value)
        {
            return new Result<T>(value);
        }
    }

    public class Result : Result<object>
    {
        public Result()
        {

        }
        public Result(Exception error)
            : base(error)
        {
        }
        public Result(Exception error, string errorSource)
             : base(error, errorSource)
        {
        }
        public Result(object value)
            : base(value)
        {
        }

        //public static Result Fail(Exception error)
        //{
        //    return new Result(error);
        //}
        //public static Result Fail(Exception error, string errorSource)
        //{
        //    return new Result(error, errorSource);
        //}
        //public static Result Fail(string error)
        //{
        //    return new Result(new Exception(error));
        //}
        //public static Result Success(string process = null)
        //{
        //    return new Result(process);
        //}
        //public static Result Success(object value)
        //{
        //    return new Result(value);
        //}

        public new string ToString()
        {
            return HasError ? Error.Message : Value.ToString();
        }
    }

}