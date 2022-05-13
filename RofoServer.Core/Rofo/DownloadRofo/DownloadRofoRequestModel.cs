using MediatR;
using System;
using System.ComponentModel.DataAnnotations;

namespace RofoServer.Core.Rofo.DownloadRofo;

public class DownloadRofoRequestModel : IRequest<DownloadRofoRequestModel>
{
    [Required(ErrorMessage = "Need photo id")]
    public string PhotoId { get; set; }
    
    public string Email { get; set; }

}