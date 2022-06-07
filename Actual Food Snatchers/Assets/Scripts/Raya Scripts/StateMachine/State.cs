//ABSTRACT STATE - prototype for concrete states
using UnityEngine;

namespace Raya
{
    public abstract class State<T>
    {
        public abstract void Enter(T t);

        public abstract void Update(T t);

        public abstract void OnCollisionEnter(T t);
    }

}