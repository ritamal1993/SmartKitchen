using System;
using System.Web;

namespace SmartKitchen3.Utils
{
    public static class Cookies
    {

        public static string Get(string key)
        {
            if (HttpContext.Current.Request.Cookies.Get(key) != null)
                return HttpContext.Current.Request.Cookies.Get(key).Value;
            else return null;
        }

        public static void Set(string key, string value, int? expireTime)
        {
            if (Get(key) == null)
            {
                HttpCookie cookie = new HttpCookie(key);
                cookie.Value = value;

                if (expireTime.HasValue)
                    cookie.Expires = DateTime.Now.AddMinutes(expireTime.Value);
                else
                    cookie.Expires = DateTime.Now.AddMinutes(10);

                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }
        /// <summary>  
        /// Delete the key  
        /// </summary>  
        /// <param name="key">Key</param>  
        public static void Remove(string key)
        {
            //HttpContext.Current.Response.Cookies.Remove(key);
            //HttpContext.Current.Request.Cookies.Remove(key);
            HttpContext.Current.Response.Cookies.Get(key).Expires = DateTime.Now.AddMinutes(-10);
            HttpContext.Current.Request.Cookies.Get(key).Expires = DateTime.Now.AddMinutes(-10);
        }
    }
}
