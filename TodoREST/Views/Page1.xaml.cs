using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TodoREST.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page1 : ContentPage
    {
        
        public Page1()
        {
            InitializeComponent();
        }

        private async void Button_ClickedAsync(object sender, EventArgs e)
        {
            //Instancio los objetos
            HttpClient client;
            client = new HttpClient();
            List<TodoItem> todoItems = new List<TodoItem>();

            //Armo la uri
            Uri uri = new Uri("http://sapientiaperu-001-site3.dtempurl.com/api/todoitems/Get");

            //Llamo al servicio
            HttpResponseMessage Respuesta = await client.GetAsync(uri);


            //Evaluo la respuesta
            if (Respuesta.IsSuccessStatusCode)
            {
                //Obtengo el contenido en json
                string json = await Respuesta.Content.ReadAsStringAsync();

                //Convertirlo en lista de objetos
                todoItems= JsonConvert.DeserializeObject<List<TodoItem>>(json);

                ltvItems.ItemsSource = todoItems;

            }
            

        }

        private async void Button_ClickedAsync2(object sender, EventArgs e)
        {
            TodoItem todoItem = new TodoItem();
            HttpClient client;
            client = new HttpClient();
            Uri uri = new Uri("http://sapientiaperu-001-site3.dtempurl.com/api/todoitems/Create");

            todoItem.ID = (Guid.NewGuid()).ToString();
            todoItem.Name = txtName.Text;
            todoItem.Notes = txtNotes.Text;
            todoItem.Done = chkDone.IsChecked;

            var json = JsonConvert.SerializeObject(todoItem);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage Respuesta = await client.PostAsync(uri, content);


            if (Respuesta.IsSuccessStatusCode)
            {
                await DisplayAlert("Alert", "Guardo Exitosamente", "OK");
            }
            else
            {
                await DisplayAlert("Alert", "Error", "Error");
            }



        }
    }
}