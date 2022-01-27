using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace TodoREST
{
	public class App : Application
	{
		public static TodoItemManager TodoManager { get; private set; }
		/// <summary>
		/// Coment test
		/// </summary>
		public App ()
		{
			TodoManager = new TodoItemManager (new RestService ());
			//MainPage = new NavigationPage(new TodoListPage());
			//MainPage = new NavigationPage(new Views.Example());
			MainPage = new NavigationPage(new Views.Page1());
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}

