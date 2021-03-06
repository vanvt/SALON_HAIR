﻿using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SALON_HAIR_CORE.Utilities
{
   
    internal static class Check
    {
        [ContractAnnotation("value:null => halt")]
        public static T NotNull<T>([NoEnumeration] T value, [InvokerParameterName, NotNull] string parameterName)
        {
            if ((object)value == null)
            {
                Check.NotEmpty(parameterName, nameof(parameterName));
                throw new ArgumentNullException(parameterName);
            }
            return value;
        }

        [ContractAnnotation("value:null => halt")]
        public static IReadOnlyList<T> NotEmpty<T>(IReadOnlyList<T> value, [InvokerParameterName, NotNull] string parameterName)
        {
            Check.NotNull<IReadOnlyList<T>>(value, parameterName);
            if (value.Count == 0)
            {
                Check.NotEmpty(parameterName, nameof(parameterName));
                throw new ArgumentException(AbstractionsStrings.CollectionArgumentIsEmpty((object)parameterName));
            }
            return value;
        }

        [ContractAnnotation("value:null => halt")]
        public static string NotEmpty(string value, [InvokerParameterName, NotNull] string parameterName)
        {
            Exception exception = (Exception)null;
            if (value == null)
                exception = (Exception)new ArgumentNullException(parameterName);
            else if (value.Trim().Length == 0)
                exception = (Exception)new ArgumentException(AbstractionsStrings.ArgumentIsEmpty((object)parameterName));
            if (exception != null)
            {
                Check.NotEmpty(parameterName, nameof(parameterName));
                throw exception;
            }
            return value;
        }

        public static string NullButNotEmpty(string value, [InvokerParameterName, NotNull] string parameterName)
        {
            if (value != null && value.Length == 0)
            {
                Check.NotEmpty(parameterName, nameof(parameterName));
                throw new ArgumentException(AbstractionsStrings.ArgumentIsEmpty((object)parameterName));
            }
            return value;
        }

        public static IReadOnlyList<T> HasNoNulls<T>(IReadOnlyList<T> value, [InvokerParameterName, NotNull] string parameterName) where T : class
        {
            Check.NotNull<IReadOnlyList<T>>(value, parameterName);
            IReadOnlyList<T> source = value;
            Func<T, bool> func = (Func<T, bool>)(e => (object)e == null);
            //Func<T, bool> predicate;
            if (source.Any<T>(func))
            {
                Check.NotEmpty(parameterName, nameof(parameterName));
                throw new ArgumentException(parameterName);
            }
            return value;
        }
    }
}
