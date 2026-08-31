using System;

namespace OpenCVForUnity.CoreModule
{

    /// <summary>
    /// The exception that is thrown by OpenCVForUntiy. 
    /// </summary>
    public class CvException : ApplicationException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CvException"/> class.
        /// </summary>
        public CvException()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CvException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">
        /// The message that describes the error.
        /// </param>
        public CvException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CvException"/> class with a formatted error message.
        /// </summary>
        /// <param name="messageFormat">
        /// The composite format string that contains text intermixed with format items.
        /// </param>
        /// <param name="args">
        /// An array of objects to format.
        /// </param>
        public CvException(string messageFormat, params object[] args)
            : base(string.Format(messageFormat, args))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CvException"/> class with a specified error message and a reference to the inner exception that caused this exception.
        /// </summary>
        /// <param name="message">
        /// The message that describes the error.
        /// </param>
        /// <param name="innerException">
        /// The exception that is the cause of the current exception, or a null reference if no inner exception is specified.
        /// </param>
        public CvException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
