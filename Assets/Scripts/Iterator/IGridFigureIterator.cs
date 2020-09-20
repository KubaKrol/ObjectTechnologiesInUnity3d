public interface IGridFigureIterator
{
    GridFigure GetNext();
    bool HasMore();
    void UpdateCache();
}
