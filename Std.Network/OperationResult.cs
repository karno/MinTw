
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Std.Network
{
    /// <summary>
    /// Result of operation
    /// </summary>
    /// <typeparam name="T">Data type</typeparam>
    public class OperationResult<T>
    {
        /// <summary>
        /// Target uri
        /// </summary>
        public Uri Target { get; private set; }

        /// <summary>
        /// Is succeeded operation
        /// </summary>
        public bool Succeeded { get; private set; }

        /// <summary>
        /// Thrown exception
        /// </summary>
        public Exception Exception { get; private set; }

        /// <summary>
        /// Description message (optional)
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        /// Returned data
        /// </summary>
        public T Data { get; private set; }

        private OperationResult(Uri target, bool succeeded, T data, string message, Exception thrown)
        {
            this.Target = target;
            this.Succeeded = succeeded;
            this.Data = data;
            this.Message = message;
            this.Exception = thrown;
        }

        /// <summary>
        /// Succeeded result
        /// </summary>
        /// <param name="target">target</param>
        /// <param name="data">return data</param>
        /// <param name="message">message</param>
        public OperationResult(Uri target, T data, string message)
            : this(target, true, data, message, null) { }

        /// <summary>
        /// Succeeded result
        /// </summary>
        /// <param name="target">target</param>
        /// <param name="data">return data</param>
        public OperationResult(Uri target, T data)
            : this(target, data, null) { }

        public OperationResult(Uri target, Exception thrown)
            : this(target, thrown, thrown.Message) { }

        /// <summary>
        /// Failed result
        /// </summary>
        /// <param name="target">target uri</param>
        /// <param name="thrown">thrown exception</param>
        /// <param name="message">returning message</param>
        public OperationResult(Uri target, Exception thrown, string message)
            : this(target, false, default(T), message, thrown) { }

        /// <summary>
        /// Custom result
        /// </summary>
        /// <param name="target">target uri</param>
        /// <param name="succeeded">succeeded flag</param>
        /// <param name="data">link data</param>
        /// <param name="message">message</param>
        public OperationResult(Uri target, bool succeeded, T data, string message)
            : this(target, succeeded, data, message, null) { }
    }
}
