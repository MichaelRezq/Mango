using Mango.Web.Models;

namespace Mango.Web.Service.IService
{
    public interface IBaseService
    {
        // when i meet any api calls i will pass in the paramater the requestDto and the response will be ResponseDto
        Task <ResponseDto?> SendAsync (RequestDto requestDto);
    }
}
