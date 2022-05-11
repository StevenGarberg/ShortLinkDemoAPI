﻿using ShortLink.API.Models.Requests;
using ShortLink.API.Models.Responses;

namespace ShortLink.API.Services;

public interface IUrlService
{
    /// <summary>
    /// Encodes a user supplied URL
    /// </summary>
    /// <param name="request">URL Request</param>
    /// <returns>URL Response</returns>
    Task<UrlResponse> Encode(UrlRequest request);
    
    /// <summary>
    /// Decodes a user supplied URL, if it exists in the data store
    /// </summary>
    /// <param name="request">URL Request</param>
    /// <returns>URL Response</returns>
    Task<UrlResponse> Decode(UrlRequest request);
}

public class UrlService : IUrlService
{
    public Task<UrlResponse> Encode(UrlRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<UrlResponse> Decode(UrlRequest request)
    {
        throw new NotImplementedException();
    }
}