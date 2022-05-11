using System.ComponentModel.DataAnnotations;
using MediatR;

namespace RofoServer.Core.Rofo.GetAllComments;

public class GetAllCommentsRofoRequestModel : IRequest<GetAllCommentsRofoRequestModel>
{
    public string Email { get; set; }

    [Required(ErrorMessage = "Photo required")]
    public string PhotoId { get; set; }

}