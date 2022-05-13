using System;
using MediatR;
using System.ComponentModel.DataAnnotations;
using RofoServer.Core.Utils.Validators;

namespace RofoServer.Core.Rofo.UploadRofo;

public class UploadRofoRequestModel : IRequest<UploadRofoRequestModel>
{
    [Required(ErrorMessage = "Group required")]
    public Guid GroupId { get; set; }
    
    public string Description { get; set; }

    [Required(ErrorMessage = "Please upload image")]
    [SizeCheck(ErrorMessage = "File is too big")]
    public string Photo { get; set; }
    
    public string Email { get; set; }

}