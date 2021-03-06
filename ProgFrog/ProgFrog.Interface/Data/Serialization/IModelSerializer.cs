﻿namespace ProgFrog.Interface.Data.Serialization
{
    public interface IModelSerializer<T>
    {
        string Serialize(T model);
        T Deserialize(string str);
    }
}