using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Raven.Manager
{
    public class CoroutinesManager : MonoBehaviour
    {
        Dictionary<object, HashSet<Coroutine>> coroutinesLookup = new Dictionary<object, HashSet<Coroutine>>();

        public Coroutine StartCoroutine(IEnumerator enumerator, object publisher)
        {
            Coroutine coroutine = StartCoroutine(enumerator);

            if (coroutinesLookup.ContainsKey(publisher))
            {
                foreach (var item in coroutinesLookup)
                {
                    if (item.Key == publisher)
                    {
                        item.Value.Add(coroutine);
                    }
                }
            }
            else
            {
                coroutinesLookup.Add(publisher, new HashSet<Coroutine>() { coroutine });
            }

            return coroutine;
        }

        public void StopCoroutine(Coroutine coroutine, object publisher)
        {
            if (coroutine == null) return;

            if (coroutinesLookup.ContainsKey(publisher))
            {
                foreach (var item in coroutinesLookup)
                {
                    if (item.Key == publisher)
                    {
                        Coroutine coroutineToStop = item.Value.FirstOrDefault(x => x == coroutine);

                        if (coroutineToStop != null)
                            StopCoroutine(coroutineToStop);
                    }
                }
            }
        }

        public void StopAllCoroutines(object publisher)
        {
            if (coroutinesLookup.ContainsKey(publisher))
            {
                foreach (var item in coroutinesLookup)
                {
                    if (item.Key == publisher)
                    {
                        foreach (Coroutine coroutine in item.Value)
                        {
                            StopCoroutine(coroutine);
                        }
                    }
                }
            }
        }

        public void DestroyPublisher(object publisher)
        {
            if (coroutinesLookup.ContainsKey(publisher))
            {
                coroutinesLookup.Remove(publisher);
            }
        }
    }
}

