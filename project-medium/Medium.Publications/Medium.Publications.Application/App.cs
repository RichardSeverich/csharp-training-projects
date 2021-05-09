using Medium.Publications.Services;

namespace Medium.Publications.Application
{
    public class App
    {
        public PublicationServices publicationServices { get; set; }

        public App(PublicationServices publicationServices)
        {
            this.publicationServices = publicationServices;
        }
        
    }
}
