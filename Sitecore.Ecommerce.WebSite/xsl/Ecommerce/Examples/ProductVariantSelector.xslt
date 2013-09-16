<?xml version="1.0" encoding="UTF-8"?>

<!--=============================================================
    File: ProductVariantSelector.xslt                                                   
    Created by: sitecore\admin                                       
    Created: 15.06.2009 21:19:20                                               
    Copyright notice at bottom of file
==============================================================-->

<xsl:stylesheet version="1.0"
  xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
  xmlns:sc="http://www.sitecore.net/sc"
  xmlns:dot="http://www.sitecore.net/dot"
  xmlns:ec="http://www.sitecore.net/ec"
  exclude-result-prefixes="dot sc">

    <xsl:variable name="SelectedValue" select="'selected'"/>
  <!--==============================================================-->
  <!-- p and s variant                                              -->
  <!--==============================================================-->
  <xsl:template match="*[@template='p and s variant']" mode="product-variant-selector">
    <xsl:variable name="variants" select="ec:GetAvalibleVariantsXml('color,size',.)/root"/>

    <xsl:value-of select="ec:Translate('Color')"/>:
    <select onchange="location.href=this.value">
      <xsl:for-each select="$variants/variant[@field='color']">
        <option value="{@value}">
          <xsl:if test="@selected">
            <xsl:attribute name="selected">
                <xsl:value-of select="$SelectedValue"/>
            </xsl:attribute>
          </xsl:if>
          <xsl:value-of select="@text"/>
        </option>
      </xsl:for-each>

    </select>

      <xsl:value-of select="ec:Translate('Size')"/>:
      <select onchange="location.href=this.value">
      <xsl:for-each select="$variants/variant[@field='size']">
        <option value="{@value}">
          <xsl:if test="@selected">
            <xsl:attribute name="selected">
                <xsl:value-of select="$SelectedValue"/>
            </xsl:attribute>
          </xsl:if>
          <xsl:value-of select="@text"/>
        </option>
      </xsl:for-each>

    </select>

  </xsl:template>

  <!--==============================================================-->
  <!-- p and s                                                      -->
  <!--==============================================================-->
  <xsl:template match="*[@template='p and s']" mode="product-variant-selector">
    <xsl:value-of select="@template"/>
  </xsl:template>


</xsl:stylesheet>