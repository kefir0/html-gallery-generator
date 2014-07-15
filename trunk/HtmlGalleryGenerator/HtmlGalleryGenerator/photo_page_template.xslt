<?xml version="1.0" encoding="utf-8"?>

<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl">
  <xsl:output method="html" indent="yes" omit-xml-declaration="yes" />

  <xsl:template match="/">
    <xsl:text disable-output-escaping='yes'>&lt;!DOCTYPE html></xsl:text>
    <html>
      <head>
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
        <title>
          <xsl:value-of select="//Title" />
        </title>
        <link href="trialru.css" rel="stylesheet" media="all" />
        <script src="http://code.jquery.com/jquery-1.9.1.min.js">
          <xsl:comment></xsl:comment>
        </script>
        <script>
          $(document).ready(function () {
          AdjustAllPictures();
          $("img.photo3").click(function () {
          var maxHeightCss = $(this).css("max-height");
          if (maxHeightCss != "none")
          {
          ClearSize(this);
          }
          else
          {
          AdjustSize(this);
          }
          });
          });

          $(window).resize(function () {
          AdjustAllPictures();
          });

          function AdjustAllPictures()
          {
          $("img.photo3").each(function (index, element) {
          AdjustSize(element);
          });
          }

          function AdjustSize(element)
          {
          $(element).css("max-height", $(window).height() - 20);
          $(element).css("max-width", $(window).width() - 20);
          }

          function ClearSize(element)
          {
          $(element).css("max-height", "");
          $(element).css("max-width", "");
          }

        </script>
      </head>
      <body>
        <div class="mainContent">
          <div class="logo">
            <img src="logo.png" />
          </div>

          <p>
            <span class="pages">Страницы: </span>
            <xsl:for-each select="//Page">
              <xsl:choose>
                <xsl:when test="id = /Gallery/PageId">
                  <span class="pageNum" style="font-weight:bold">
                    <xsl:value-of select="id" />
                  </span>
                </xsl:when>
                <xsl:otherwise>
                  <a href="{url}" class="pageNum">
                    <xsl:value-of select="id" />
                  </a>
                </xsl:otherwise>
              </xsl:choose>
            </xsl:for-each>
          </p>

          <xsl:for-each select="//Photo">
            <div class="photoAndText3">
              <img src="{src}" class="photo3" />
              <span class="text">
                <xsl:value-of select="id" />
              </span>
            </div>
          </xsl:for-each>

          <p>
            <span class="pages">Страницы: </span>
            <xsl:for-each select="//Page">
              <xsl:choose>
                <xsl:when test="id = /Gallery/PageId">
                  <span class="pageNum" style="font-weight:bold">
                    <xsl:value-of select="id" />
                  </span>
                </xsl:when>
                <xsl:otherwise>
                  <a href="{url}" class="pageNum">
                    <xsl:value-of select="id" />
                  </a>
                </xsl:otherwise>
              </xsl:choose>
            </xsl:for-each>
          </p>

          <p>

<!-- Yandex.Metrika informer -->
<a href="http://metrika.yandex.ru/stat/?id=5071168&amp;from=informer"
target="_blank" rel="nofollow"><img src="//bs.yandex.ru/informer/5071168/3_0_FFFFFFFF_EFEFEFFF_0_pageviews"
style="width:88px; height:31px; border:0; display:table; margin:auto" alt="Яндекс.Метрика" title="Яндекс.Метрика: данные за сегодня (просмотры, визиты и уникальные посетители)" /></a>
<!-- /Yandex.Metrika informer -->

<!-- Yandex.Metrika counter -->
<div style="display:none;"><script type="text/javascript">
(function(w, c) {
    (w[c] = w[c] || []).push(function() {
        try {
            w.yaCounter5071168 = new Ya.Metrika({id:5071168,
                    clickmap:true,
                    trackLinks:true});
        }
        catch(e) { }
    });
})(window, 'yandex_metrika_callbacks');
</script></div>

<script src="//mc.yandex.ru/metrika/watch.js" type="text/javascript" defer="defer"></script>
<noscript><div><img src="//mc.yandex.ru/watch/5071168" style="position:absolute; left:-9999px;" alt="" /></div></noscript>
<!-- /Yandex.Metrika counter -->

			</p>

        </div>
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>
