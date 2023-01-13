namespace CoinApi.Shared
{
    public enum ApiErrorCode
    {
        Validation = 1,
        Error = 2
    }

    public static class ApiFunctions
    {
        public static ApiResponse ApiSuccessResponses(dynamic responseData, string sucdessMessage)
        {
            return new ApiResponse
            {
                IsSuccess = true,
                ResponseData = responseData,
                Message = sucdessMessage
            };
        }

        public static ApiResponse ApiPartialSuccessResponses(dynamic responseData, string sucdessMessage)
        {
            return new ApiResponse
            {
                IsSuccess = false,
                ResponseData = responseData,
                Message = sucdessMessage
            };
        }

        public static ApiResponse ApiSuccessResponse(dynamic responseData)
        {
            return new ApiResponse
            {
                IsSuccess = true,
                ResponseData = responseData,
            };
        }

        public static ApiResponse ApiValidationResponse(string validationMessage)
        {
            return new ApiResponse
            {
                IsSuccess = false,
                ErrorCode = ApiErrorCode.Validation,
                Message = validationMessage
            };
        }

        public static ApiResponse ApiErrorResponse(string errorMessage)
        {
            return new ApiResponse
            {
                IsSuccess = false,
                ErrorCode = ApiErrorCode.Error,
                Message = errorMessage
            };
        }

        public static ApiResponse ApiErrorResponse(Exception ex)
        {
            return new ApiResponse
            {
                IsSuccess = false,
                ErrorCode = ApiErrorCode.Error,
                Message = ex.ToString()
            };
        }
    }

}
