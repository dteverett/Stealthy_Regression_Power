using System;

namespace VendorUploadService
{
    public class NullReturnedValueException : Exception
    {
        public NullReturnedValueException()
        {
        }

        public NullReturnedValueException(string message)
            : base(message)
        {
        }
    }
}