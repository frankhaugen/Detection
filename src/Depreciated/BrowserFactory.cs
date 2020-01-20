// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;
using Wangkanai.Detection.Extensions;
using Wangkanai.Detection.Models;

namespace Wangkanai.Detection
{
    public class BrowserFactory : IBrowserFactory
    {
        public BrowserFactory() { }

        public BrowserFactory(Browser browserType)
            => Type = browserType;

        public BrowserFactory(Browser browserType, Version version)
            : this(browserType)
            => Version = version;

        public BrowserFactory(string name)
        {
            if (!Enum.TryParse(name, true, out Browser type))
                throw new BrowserNotFoundException(name, "not found");

            Type = type;
        }

        public string? Name { get; set; }
        public string? Maker { get; set; }
        public Browser Type { get; set; } = Browser.Others;
        public Version? Version { get; set; }

        protected static Version GetVersion(string agent, string browser)
        {
            var first = agent.IndexOf(browser);
            string cut;
            try
            {
                cut = agent.Substring(first + browser.Length + 1);
            }
            catch
            {
                cut = agent.Substring(first + browser.Length);
            }

            var version = cut.Contains(" ") ? cut.Substring(0, cut.IndexOf(' ')) : cut;
            return version.ToVersion();
        }
    }
}