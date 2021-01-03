using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Solutions
{
    public class ProblemC_solved
    {
        public static void Main()
        {
            Main(Console.In);
        }

        public static void Main(TextReader input)
        {
            var n = int.Parse(input.ReadLine());
            var h = input.ReadLine().Split().Select(long.Parse).ToArray();
            var m = int.Parse(input.ReadLine());
            var field = new Field(h);
            for (int i = 0; i < m; i++)
            {
                var change = input.ReadLine().Split().Select(long.Parse).ToArray();
                field.Change((int)change[0] - 1, change[1]);
                Console.Out.WriteLine(field.lengthsHeap.Last() + 1);
            }
        }

        class Field
        {
            public long[] h;
            public readonly Tree starts = new Tree();
            public readonly Dictionary<int, int> lengths = new Dictionary<int, int>();
            public readonly Dictionary<int, int> lengthsCounts = new Dictionary<int, int>();
            public readonly Tree lengthsHeap = new Tree();

            public Field(long[] h)
            {
                this.h = h;
                for (int i = 0; i < h.Length; i++)
                {
                    if (IsStartPoint(i))
                        AddStart(i);
                }
            }

            private void RemoveStart(int start)
            {
                var prevStart = starts.Prev(start);
                var nextStart = starts.Next(start);
                if (prevStart != -1 && nextStart != -1)
                {
                    RemoveLength(lengths[prevStart]);
                    lengths[prevStart] = lengths[prevStart] + lengths[start];
                    AddLength(lengths[prevStart]);
                }
                else if (prevStart != -1)
                {
                    RemoveLength(lengths[prevStart]);
                    lengths[prevStart] = 0;
                    AddLength(lengths[prevStart]);
                }

                RemoveLength(lengths[start]);
                starts.Remove(start);
                lengths.Remove(start);
            }

            private void AddStart(int start)
            {
                var prevStart = starts.Prev(start);
                if (prevStart != -1)
                {
                    RemoveLength(lengths[prevStart]);
                    lengths[prevStart] = start - prevStart;
                    AddLength(lengths[prevStart]);
                }

                var nextStart = starts.Next(start);
                if (nextStart != -1)
                {
                    starts.Add(start);
                    lengths[start] = nextStart - start;
                    AddLength(lengths[start]);
                }
                else
                {
                    starts.Add(start);
                    lengths[start] = 0;
                    AddLength(lengths[start]);
                }
            }

            public void Change(int i, long nh)
            {
                if (IsStartPoint(i))
                    RemoveStart(i);
                if (i > 0 && IsStartPoint(i - 1))
                    RemoveStart(i - 1);
                if (i < h.Length - 1 && IsStartPoint(i + 1))
                    RemoveStart(i + 1);

                h[i] = nh;

                if (IsStartPoint(i))
                    AddStart(i);
                if (i > 0 && IsStartPoint(i - 1))
                    AddStart(i - 1);
                if (i < h.Length - 1 && IsStartPoint(i + 1))
                    AddStart(i + 1);
            }

            private void AddLength(int length)
            {
                if (lengthsCounts.ContainsKey(length))
                    lengthsCounts[length]++;
                else
                {
                    lengthsCounts[length] = 1;
                    lengthsHeap.Add(length);
                }
            }

            private void RemoveLength(int length)
            {
                lengthsCounts[length]--;
                if (lengthsCounts[length] == 0)
                {
                    lengthsCounts.Remove(length);
                    lengthsHeap.Remove(length);
                }
            }

            private bool IsStartPoint(int i) => i == 0 || i == h.Length - 1 || h[i - 1] > h[i] && h[i + 1] > h[i];
        }

        class Tree
        {
            private Item root;
            private readonly Random random = new Random();

            public void Add(int key)
            {
                Insert(ref root, new Item(key, random.Next()));
            }

            public void Remove(int key)
            {
                Erase(ref root, key);
            }

            public int SearchLE(int key)
            {
                var item = FindLE(root, key);
                return item?.key ?? -1;
            }

            public int SearchGE(int key)
            {
                var item = FindLE(root, key);
                return item?.key ?? -1;
            }

            public int Next(int key)
            {
                var item = FindGE(root, key + 1);
                return item?.key ?? -1;
            }

            public int Prev(int key)
            {
                var item = FindLE(root, key - 1);
                return item?.key ?? -1;
            }

            public int Last()
            {
                var item = Last(root);
                return item.key;
            }

            public int First()
            {
                var item = First(root);
                return item.key;
            }

            class Item
            {
                public int key;
                public int prior;
                public Item l, r;

                public Item()
                {
                }

                public Item(int key, int prior)
                {
                    this.key = key;
                    this.prior = prior;
                }
            }

            void Split(Item t, int key, out Item l, out Item r)
            {
                if (t == null)
                    l = r = null;
                else if (key < t.key)
                {
                    Split(t.l, key, out l, out t.l);
                    r = t;
                }
                else
                {
                    Split(t.r, key, out t.r, out r);
                    l = t;
                }
            }

            void Insert(ref Item t, Item it)
            {
                if (t == null)
                    t = it;
                else if (it.prior > t.prior)
                {
                    Split(t, it.key, out it.l, out it.r);
                    t = it;
                }
                else
                {
                    if (it.key < t.key)
                        Insert(ref t.l, it);
                    else
                        Insert(ref t.r, it);
                }
            }

            void Merge(ref Item t, Item l, Item r)
            {
                if (l == null || r == null)
                    t = l ?? r;
                else if (l.prior > r.prior)
                {
                    Merge(ref l.r, l.r, r);
                    t = l;
                }
                else
                {
                    Merge(ref r.l, l, r.l);
                    t = r;
                }
            }

            void Erase(ref Item t, int key)
            {
                if (t == null)
                    return;
                if (t.key == key)
                    Merge(ref t, t.l, t.r);
                else
                {
                    if (key < t.key)
                        Erase(ref t.l, key);
                    else
                        Erase(ref t.r, key);
                }
            }

            Item FindLE(Item t, int key)
            {
                if (t == null)
                    return null;

                if (t.key == key)
                    return t;

                if (t.key > key)
                    return FindLE(t.l, key);

                return FindLE(t.r, key) ?? t;
            }

            Item FindGE(Item t, int key)
            {
                if (t == null)
                    return null;

                if (t.key == key)
                    return t;

                if (t.key < key)
                    return FindGE(t.r, key);

                return FindGE(t.l, key) ?? t;
            }

            Item Last(Item t)
            {
                if (t == null)
                    return null;
                while (t.r != null)
                    t = t.r;
                return t;
            }

            Item First(Item t)
            {
                if (t == null)
                    return null;
                while (t.l != null)
                    t = t.l;
                return t;
            }

            Item Unite(Item l, Item r)
            {
                if (l == null || r == null)
                    return l ?? r;
                if (l.prior < r.prior)
                {
                    var t = l;
                    l = r;
                    r = t;
                }

                Item lt, rt;
                Split(r, l.key, out lt, out rt);
                l.l = Unite(l.l, lt);
                l.r = Unite(l.r, rt);
                return l;
            }
        }
    }
}