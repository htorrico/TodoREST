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
    public partial class Example : ContentPage
    {        
        HttpClient client;
        public List<TodoItem> Items { get; private set; }
        public Example()
        {
            InitializeComponent();

 
            client = new HttpClient();

            



        }

        public async Task<List<TodoItem>> Get()
        {

            //Repositorio de mi respuesta
            Items = new List<TodoItem>();

            //Armo la url del servicio            
            Uri uri = new Uri(string.Format(Constants.RestUrl, "Get"));            

            HttpResponseMessage response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                Items = JsonConvert.DeserializeObject<List<TodoItem>>(content);

            }
            return Items;
        }


        public async Task Save(TodoItem item, bool isNewItem = false)
        {


            var json = JsonConvert.SerializeObject(item);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = null;
            if (isNewItem)
            {
                var uri = new Uri(string.Format(Constants.RestUrl, "Create"));
                response = await client.PostAsync(uri, content);
            }
            else
            {
                var uri = new Uri(string.Format(Constants.RestUrl, "Edit"));
                response = await client.PutAsync(uri, content);
            }


            if (response.IsSuccessStatusCode)
            {
                //string content = await response.Content.ReadAsStringAsync();
                //Items = JsonConvert.DeserializeObject<List<TodoItem>>(content);

            }
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {

            ltvItems.ItemsSource= await Get();
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            //await Save(new TodoItem {ID=(new Guid()).ToString(), Name = "Hugo" },true);
            await Save(new TodoItem { ID = "123", Name = "Hugo", Done=true, Notes="notes" }, true);
            await DisplayAlert("Gracias","gracias","ok");
        }
    }
}