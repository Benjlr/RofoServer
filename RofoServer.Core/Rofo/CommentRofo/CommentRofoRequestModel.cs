using System.ComponentModel.DataAnnotations;
using MediatR;

namespace RofoServer.Core.Rofo.CommentRofo;

public class CommentRofoRequestModel : IRequest<CommentRofoRequestModel>
{
    public string Email { get; set; }

    [Required(ErrorMessage = "Photo required")]
    public string PhotoId { get; set; }

    [Required(ErrorMessage = "No comment given")]
    public string Text { get; set; }

}