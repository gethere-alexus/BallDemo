using System;

namespace Utils.Extensions
{
    public static class BuildExtensions
    {
        public static T With<T>(this T self, Action<T> set)
        {
            set.Invoke(self);
            return self;
        }
    }
}