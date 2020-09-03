using System.Collections.Generic;

public interface IMementoData
{
    void Save(IAmMemorized memorizedObject);
    void Load(IAmMemorized memorizedObject);
}
