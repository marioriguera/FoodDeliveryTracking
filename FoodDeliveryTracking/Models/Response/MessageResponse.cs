namespace FoodDeliveryTracking.Models.Response
{
    /// <summary>
    /// Manage message response.
    /// </summary>
    /// <typeparam name="T">Type of object response message.</typeparam>
    public record MessageResponse<T>
    {
        /// <summary>
        /// Gets or sets message response state.
        /// </summary>
        public bool? IsSuccess { get; set; }

        /// <summary>
        /// Gets or sets message.
        /// Gets or sets message.
        /// </summary>
        public T? Message { get; set; }

        /// <summary>
        /// Initialize a new instance of <see cref="MessageResponse{T}"/> record;
        /// </summary>
        private MessageResponse() { }

        /// <summary>
        /// Creates an instance of the MessageResponse class indicating a failure and an associated message.
        /// </summary>
        /// <param name="message">The message describing the cause of the failure.</param>
        /// <returns>An instance of MessageResponse indicating a failure with the specified message.</returns>
        public static MessageResponse<T> Fail(T message)
        {
            MessageResponse<T> messageResponse = new MessageResponse<T>();
            messageResponse.IsSuccess = false;
            messageResponse.Message = message;
            return messageResponse;
        }

        /// <summary>
        /// Creates an instance of the MessageResponse class indicating a success and associated content.
        /// </summary>
        /// <param name="content">The associated content or successful result.</param>
        /// <returns>A MessageResponse instance that indicates success with the specified content.</returns>
        public static MessageResponse<T> Success(T content)
        {
            MessageResponse<T> messageResponse = new MessageResponse<T>();
            messageResponse.IsSuccess = true;
            messageResponse.Message = content;
            return messageResponse;
        }
    }
}
