using System;

namespace ENet
{
    public static class Library
    {
        public static uint Time
        {
            get => throw new NotImplementedException("Library.Time not supported in LENet.");
            set => throw new NotImplementedException("Library.Time not supported in LENet.");
        }

        public static void Initialize() {}

        public static void Deinitialize() {}
    }
}