namespace geolocalizationApi.Middlewares
{
    /// <summary>
    /// Represents the error that will be return in the response body in case of an error occurs.
    /// </summary>
    public class ErrorHandler
    {
        /// <summary>
        /// A short message.
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// The details to get more info. This can be omitted in Production Mode.
        /// </summary>
        public string Details { get; set; }
    }
}
