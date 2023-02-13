namespace Helper.Lesson_7
{
	public interface IData<T>
	{
		void Save(T value);
		T Load();
		void SetOptions(string path);
	}
}