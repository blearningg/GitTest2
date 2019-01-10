using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TwitterCloneApp
{
    public class TweetViewModel
    {
        public TweetViewModel()
        {
            tweet = new Tweet();
        }

        public Tweet tweet { get; set; }
        public IEnumerable<Tweet> Tweets { get; set; }
    }
}