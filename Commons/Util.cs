using System;
using WebSiteAPI.Models.Dtos;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WepSiteAPI.Commons
{
    public class Util
    {
        public static ResponseDto<T> BuildResponse<T>(bool status, string message, ModelStateDictionary errs, T data)
        {

            var listOfErrorItems = new List<ErrorItem>();

            if (errs != null)
            {
                foreach (var err in errs)
                {
                    var key = err.Key;
                    var errValues = err.Value;
                    var errList = new List<string>();
                    foreach (var errItem in errValues.Errors)
                    {
                        errList.Add(errItem.ErrorMessage);
                        listOfErrorItems.Add(new ErrorItem { Key = key, ErrorMessages = errList });
                    }
                }
            }
            var res = new ResponseDto<T>
            {
                IsSuccessful = status,
                Message = message,
                Data = data,
                Errors = listOfErrorItems
            };

            return res;
        }
    }
}
