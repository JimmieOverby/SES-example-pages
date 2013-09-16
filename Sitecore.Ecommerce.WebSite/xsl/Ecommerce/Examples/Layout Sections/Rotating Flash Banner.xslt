<?xml version="1.0" encoding="UTF-8"?>

<!--=============================================================
    File: Rotating Flash Banner.xslt                                                   
    Created by: sitecore\admin                                       
    Created: 04.12.2009 16:57:45                                               
    Copyright notice at bottom of file
==============================================================-->

<xsl:stylesheet version="1.0"
  xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
  xmlns:sc="http://www.sitecore.net/sc"
  xmlns:dot="http://www.sitecore.net/dot"
  xmlns:ec="http://www.sitecore.net/ec"
  exclude-result-prefixes="dot sc ec">

  <!-- output directives -->
  <xsl:output method="xml" indent="no" encoding="UTF-8" omit-xml-declaration="yes" />

  <!-- parameters -->
  <xsl:param name="lang" select="'en'"/>
  <xsl:param name="id" select="''"/>
  <xsl:param name="sc_item"/>
  <xsl:param name="sc_currentitem"/>
  <xsl:param name="itemID" />

  <xsl:variable name="OneColWidth" select="ec:GetDesignSetting('One Column Width')"/>

  <!--==============================================================-->
  <!-- main                                                         -->
  <!--==============================================================-->
  <xsl:template match="*">
    <xsl:choose>
      <xsl:when test ="$itemID">
        <xsl:apply-templates mode="layout-section" select="sc:item($itemID,.)" />
      </xsl:when>
      <xsl:otherwise>
        <xsl:apply-templates mode ="layout-section" select="." />
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>

  <!--=========================================
      Column template - Rotating Flash Banner
  =============================================-->
  <xsl:template match="*[@template='rotating flash banner']" mode="layout-section">
    <xsl:variable name="mediaItem" select="sc:item(sc:fld('flash', ., 'mediaid'),.)" />
    <xsl:variable name="identifier" select="translate(@name,' ','_')"/>    
    <xsl:choose>
      <xsl:when test="not(ec:IsEditingMode())">
        <div class="content">
          <div class="colMargin8NoTopMargin">
            <!-- Get field values -->
            <xsl:variable name="src" select="sc:GetMediaUrl($mediaItem)" />
            <xsl:variable name="width" select="sc:fld('Width',$mediaItem)" />
            <xsl:variable name="height" select="sc:fld('Height',$mediaItem)" />
            <xsl:variable name="mimetype" select="sc:fld('Mime Type',$mediaItem)" />

            <script type="text/javascript">
              swfobject.embedSWF('/<xsl:value-of select="$src"/>', "<xsl:value-of select="$identifier"/>",
              "<xsl:value-of select="$OneColWidth"/>", "<xsl:value-of select="$height"/>", "8.0.0", '/jscript/expressInstall.swf', {xmlPath : '<xsl:value-of select="sc:path(.)"/>'},
              {wmode : 'transparent', bgcolor : '#ffffff'});
            </script>

            <div id="{$identifier}">
              <sc:dot/>
              <a href="http://www.adobe.com/go/getflashplayer">
                <img src="http://www.adobe.com/images/shared/download_buttons/get_flash_player.gif" alt="Get Adobe Flash player" />
              </a>
            </div>
          </div>
        </div>
      </xsl:when>
      <xsl:otherwise>

        <!-- Show the first image of the flash module -->
        <xsl:variable name="moduleItem" select="sc:item(sc:fld('FlashModule',.),.)" />

        <div class="content">
          <div class="colMargin8NoTopMargin">
            <div id="flashContainer" class="editMode">
              <div id="flashImage">
                <sc:image field="image1" />
              </div>
            </div>
          </div>
        </div>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>
</xsl:stylesheet>