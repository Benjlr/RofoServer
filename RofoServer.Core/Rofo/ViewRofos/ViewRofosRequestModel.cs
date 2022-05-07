using MediatR;
using System;
using System.ComponentModel.DataAnnotations;

namespace RofoServer.Core.Rofo.ViewRofos;

public class ViewRofosRequestModel : IRequest<ViewRofosRequestModel>
{
    public string Email { get; set; }

    [Required(ErrorMessage = "Group  required")]
    public Guid GroupId{ get; set; }

}