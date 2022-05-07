using System;
using System.ComponentModel.DataAnnotations;
using MediatR;

namespace RofoServer.Core.Rofo.GetRofoImage;

public class GetImageRequestModel : IRequest<GetImageRequestModel>
{
    public string Email { get; set; }

    [Required(ErrorMessage = "Id  required")]
    public Guid PhotoId{ get; set; }

}