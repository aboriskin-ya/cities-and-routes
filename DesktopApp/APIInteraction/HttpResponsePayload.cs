namespace DesktopApp.APIInteraction
{
    public class HttpResponsePayload<TPayload>
    {
        public bool IsSuccessful { get; set; }

        public TPayload Payload { get; set; }
    }
}
