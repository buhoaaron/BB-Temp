using System.Collections.Generic;

namespace DataStream
{
    [System.Serializable]
    public class Data<T>
    {
        public string name;
        public T data;
        public string lastModifyTime;

        public Data(string name, T data)
        {
            this.name = name;
            this.data = data;
        }
    }

    [System.Serializable]
    public class DataList<T>
    {
        public List<Data<T>> list;

        public int Count { get { return list.Count; } }
        public Data<T> this[int index]
        {
            get { return list[index]; }
            set { list[index] = value; }
        }

        public DataList()
        {
            list = new();
        }

        public void Add(Data<T> data)
        {
            list.Add(data);
        }

        public void RemoveAt(int index)
        {
            list.RemoveAt(index);
        }
    }
}