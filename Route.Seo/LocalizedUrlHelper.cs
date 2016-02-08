using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace Routes.Seo
{
    public class LocalizedUrlHelper
    {

        public LocalizedUrlHelper(HttpRequestBase httpRequest, bool rawUrl = false)
            : this(httpRequest.ApplicationPath, rawUrl ? httpRequest.RawUrl : httpRequest.AppRelativeCurrentExecutionFilePath, rawUrl)
        {
            Guard.ArgumentNotNull(() => httpRequest);
        }

        public LocalizedUrlHelper(string applicationPath, string relativePath, bool rawUrl = false)
        {
            Guard.ArgumentNotNull(() => applicationPath);
            Guard.ArgumentNotNull(() => relativePath);

            this.ApplicationPath = applicationPath;

            if (rawUrl)
            {
                this.RelativePath = RemoveApplicationPathFromRawUrl(relativePath).TrimStart('~', '/');
            }
            else
            {
                this.RelativePath = relativePath.TrimStart('~', '/');
            }
        }

        public string ApplicationPath { get; private set; }

        public string RelativePath { get; private set; }

        public bool IsLocalizedUrl()
        {
            string seoCode;
            return IsLocalizedUrl(out seoCode);
        }

        public bool IsLocalizedUrl(out string seoCode)
        {
            seoCode = null;

            string firstPart = this.RelativePath;

            if (string.IsNullOrEmpty(firstPart))
            {
                return false;
            }

            int firstSlash = firstPart.IndexOf('/');

            if (firstSlash > 0)
            {
                firstPart = firstPart.Substring(0, firstSlash);
            }

            //int length = firstPart.Length;
            //if ((length == 2 || length == 5) && RegularExpressions.IsCultureCode.IsMatch(firstPart))
            //{
            //    seoCode = firstPart;
            //    return true;
            //}
            if (IsValidCulture(firstPart))
            {
                seoCode = firstPart;
                return true;
            }

            return false;
        }

        private bool IsValidCulture(string cultureName)
        {
            var segments = cultureName.Split('-');

            if (segments.Length == 0)
            {
                return false;
            }

            if (segments.Length > 2)
            {
                return false;
            }

            if (segments.Any(s => s.Length != 2))
            {
                return false;
            }

            return true;
        }

        public string StripSeoCode()
        {
            string seoCode;
            if (IsLocalizedUrl(out seoCode))
            {
                this.RelativePath = this.RelativePath.Substring(seoCode.Length).TrimStart('/');
                //if (this.RelativePath.IsEmpty())
                //{
                //    this.RelativePath = "/";
                //}
            }

            return this.RelativePath;
        }

        public string PrependSeoCode(string seoCode, bool safe = false)
        {
            if (safe)
            {
                string currentSeoCode;
                if (IsLocalizedUrl(out currentSeoCode))
                {
                    if (seoCode == currentSeoCode)
                    {
                        return this.RelativePath;
                    }
                    else
                    {
                        StripSeoCode();
                    }
                }
            }

            this.RelativePath = String.Format( "{0}/{1}" ,seoCode, this.RelativePath);
            return this.RelativePath;
        }

        public string GetAbsolutePath()
        {
            string path = EnsureEndsWith(this.ApplicationPath,"/");
            path = path + this.RelativePath;
            if (path.Length > 1 && path[0] != '/')
            {
                path = "/" + path;
            }
            return path;
        }
        public  string EnsureEndsWith( string value, string endWith)
        {
            Guard.ArgumentNotNull(value, "value");
            Guard.ArgumentNotNull(endWith, "endWith");

            if (value.Length >= endWith.Length)
            {
                if (string.Compare(value, value.Length - endWith.Length, endWith, 0, endWith.Length, StringComparison.OrdinalIgnoreCase) == 0)
                    return value;

                string trimmedString = value.TrimEnd(null);

                if (string.Compare(trimmedString, trimmedString.Length - endWith.Length, endWith, 0, endWith.Length, StringComparison.OrdinalIgnoreCase) == 0)
                    return value;
            }

            return value + endWith;
        }



        private string RemoveApplicationPathFromRawUrl(string rawUrl)
        {
            if (rawUrl.Length == ApplicationPath.Length)
            {
                return "/";
            }

            var result = rawUrl.Substring(ApplicationPath.Length);
            // raw url always starts with '/'
            if (!result.StartsWith("/"))
            {
                result = "/" + result;
            }
            return result;
        }

    }
}