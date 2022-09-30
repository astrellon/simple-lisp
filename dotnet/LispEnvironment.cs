using System;
using System.Collections.Generic;

namespace DotnetLisp
{
    public class LispEnvironment : ILispEnvironment
    {
        #region Fields
        private readonly Dictionary<string, object> data = new Dictionary<string, object>();
        private readonly ILispEnvironment? parent;
        public bool IsReadOnly;
        #endregion

        #region Constructor
        public LispEnvironment(ILispEnvironment? parent = null)
        {
            this.IsReadOnly = false;
            this.parent = parent;
        }
        #endregion

        #region Methods
        public void Set(string key, object value)
        {
            if (this.IsReadOnly)
            {
                throw new Exception("Oh no!");
            }
            this.data[key] = value;
        }

        public object Get(string key)
        {
            if (this.data.TryGetValue(key, out var result))
            {
                return result;
            }

            if (this.parent != null)
            {
                return this.parent.Get(key);
            }

            return null;
        }
        #endregion
    }
}