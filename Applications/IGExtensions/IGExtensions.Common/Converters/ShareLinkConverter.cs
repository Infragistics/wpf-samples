using System;
using System.Windows.Data;
using IGExtensions.Common.Controls;

namespace IGExtensions.Common.Converters
{
    public class ShareLinkConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string uri = "http://labs.infragistics.com";
            if (value != null && value is ShareLink)
            {
                var shareLink = (ShareLink)value;
                var sharePortal = parameter.ToString();

                return shareLink.ConvertToUri(sharePortal);
            }
            return new Uri(uri);
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
 


    //  <a href="http://twitter.com/intent/tweet?text=IG&url=http://labs.infragistics.com" target="_blank" title='Tweet This !'><img alt='Tweet This !' src='http://1.bp.blogspot.com/_YUvD9j84Cik/TBvM3Sb-3wI/AAAAAAAAAH0/6wuL-gO0a3U/twitter.gif' style='height:16px; width:16px; padding:0; border:0; vertical-align:middle;'/></a> 

    //<a href="http://www.facebook.com/sharer.php?u=http://labs.infragistics.com&t=IG" target="_blank" title='Share On Facebook !'><img alt='Share On Facebook !' src='http://1.bp.blogspot.com/_YUvD9j84Cik/TBvMxLns3dI/AAAAAAAAAHQ/K5W2YQeN6SY/facebook.gif' style='width:16px; height:16px; padding:0; border:0; vertical-align:middle;'/></a> 

    //<a href="https://plusone.google.com/_/+1/confirm?hl=en&url=http://labs.infragistics.com" target="_blank" title='Share On Google Plus !'><img alt='Share On Google Plus !' src='https://lh3.googleusercontent.com/-BHSTWoCj69A/TuPGW3xIRqI/AAAAAAAAARA/T1uQPACJ4xw/s16/icon-google-plus.gif' style='width:16px; height:16px; padding:0; border:0; vertical-align:middle;'/></a> 

    //<a href="http://del.icio.us/post?url=http://labs.infragistics.com&title=IG" target="_blank" title='Add To Del.icio.us !'><img alt='Add To Del.icio.us !' src='http://1.bp.blogspot.com/_YUvD9j84Cik/TBvMq5Pk_6I/AAAAAAAAAHE/L1SVY7qRcnU/delicious.gif' style='width:16px; height:16px; padding:0; border:0; vertical-align:middle;'/></a> 

    //<a href="http://www.stumbleupon.com/submit?url=http://labs.infragistics.com&title=IG" target="_blank" title='Share On StumbleUpon !'><img alt='Share On StumbleUpon !' src='https://lh3.googleusercontent.com/-uXMuiLzxoFM/T25RKdHPgoI/AAAAAAAAARg/iU-TbKOb174/s16/stumble-new.gif' style='width:16px; height:16px; padding:0; border:0; vertical-align:middle;'/></a> 

    //<a href="http://digg.com/submit?phase=2&url=http://labs.infragistics.com&title=IG" target="_blank" title='Share On Digg !'><img alt='Share On Digg !' src='http://1.bp.blogspot.com/_YUvD9j84Cik/TBvMrPRHAAI/AAAAAAAAAHI/4CsHjE_v4gg/digg.gif' style='width:16px; height:16px; padding:0; border:0; vertical-align:middle;'/></a> 

    //<a href="http://pinterest.com/pin/create/button/?url=http://labs.infragistics.com&title=IG&is_video=" target="_blank" title='Pin It !'><img alt='Pin It !' src='https://lh5.googleusercontent.com/-cPMWoc6TUt4/TymRhKbzoII/AAAAAAAAARU/llKRpqyR4O4/s16/pinterest-button.gif' style='width:16px; height:16px; padding:0; border:0; vertical-align:middle;'/></a> 

    //<a href="http://reddit.com/submit?url=http://labs.infragistics.com&title=IG" target="_blank" title='Share On Reddit !'><img alt='Share On Reddit !' src='http://1.bp.blogspot.com/_YUvD9j84Cik/TBvM2-Sw3ZI/AAAAAAAAAHk/QKghefISkNc/reddit.gif' style='width:16px; height:16px; padding:0; border:0; vertical-align:middle;'/></a> 

    //<a href="http://www.linkedin.com/shareArticle?mini=true&url=http://labs.infragistics.com&title=IG" target="_blank" title='Share On LinkedIn !'><img alt='Share On LinkedIn !' src='http://1.bp.blogspot.com/_YUvD9j84Cik/TBvMxq258TI/AAAAAAAAAHc/qtW5-9eYK8g/linkedin.gif' style='width:16px; height:16px; padding:0; border:0; vertical-align:middle;'/></a> 

    //<a href="http://www.blogger.com/blog_this.pyra?t&u=http://labs.infragistics.com&n=IG&pli=1" target="_blank" title='Post To Blogger !'><img alt='Post To Blogger !' src='http://1.bp.blogspot.com/_YUvD9j84Cik/TBvMqnnrVtI/AAAAAAAAAG8/O65EnRFSHFk/blogger.gif' style='width:16px; height:16px; padding:0; border:0; vertical-align:middle;'/></a> 

    //<a href="http://www.friendfeed.com/share?link=http://labs.infragistics.com&title=IG" target="_blank" title='Share On Friend Feed !'><img alt='Share On Friend Feed !' src='http://1.bp.blogspot.com/_YUvD9j84Cik/TBvMxcy7H8I/AAAAAAAAAHU/UfNllYiyznA/friendfeed.gif' style='width:16px; height:16px; padding:0; border:0; vertical-align:middle;'/></a> 

    //<a href="http://www.myspace.com/Modules/PostTo/Pages/?u=http://labs.infragistics.com&t=IG" target="_blank" title='Share On MySpace !'><img alt='Share On MySpace !' src='http://1.bp.blogspot.com/_YUvD9j84Cik/TBvMxxC8KZI/AAAAAAAAAHg/Z2lq57aIcFc/myspace.gif' style='width:16px; height:16px; padding:0; border:0; vertical-align:middle;'/></a> 

    // <a href="http://www.google.com/bookmarks/mark?op=add&bkmk=http://labs.infragistics.com&title=IG" target="_blank" title='Google Bookmark !'><img alt='Google Bookmark !' src='http://1.bp.blogspot.com/_YUvD9j84Cik/TBvMxXNbOVI/AAAAAAAAAHY/s9pvz5gyE9I/google.gif' style='width:16px; height:16px; padding:0; border:0; vertical-align:middle;'/></a> 

    //    <a href="http://www.printfriendly.com/print/v2?url=http://labs.infragistics.com" target="_blank" title='Create PDF And Print Friendly !'><img alt='Create PDF And Print Friendly !' src='https://lh5.googleusercontent.com/--gpZUQ0wj-M/TfT42Fr49kI/AAAAAAAAAOU/0BFGHHKlhD8/print-icon.gif' style='width:16px; height:16px; padding:0; border:0; vertical-align:middle;'/></a> 

    
}