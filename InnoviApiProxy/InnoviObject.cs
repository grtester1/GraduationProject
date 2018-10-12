using System;

namespace InnoviApiProxy
{
    public abstract class InnoviObject
    {
        protected abstract int Id { get; }

        public override bool Equals(object obj)
        {
            InnoviObject innoviObj = null;
            bool res = true;

            if(obj == null)
            {
                res = false;
            }
            else
            {
                innoviObj = obj as InnoviObject;
                if(this.Id != innoviObj.Id)
                {
                    res = false;
                }
            }

            return res;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
