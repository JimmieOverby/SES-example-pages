<?xml version="1.0" encoding="UTF-8"?>

<!--=============================================================
    File: Four Column.xslt                                                   
    Created by: sitecore\admin                                       
    Created: 07.12.2009 20:45:20                                               
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
  <xsl:param name="itemID"/>

  <!-- include fiels-->
  <xsl:include href="column.xslt"/>

  <!--==============================================================-->
  <!-- main                                                         -->
  <!--==============================================================-->
  <xsl:template match="*" >
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
      Layout secton - Four column
  =============================================-->
  <xsl:template match="*[@template='four column']" mode="layout-section">
    <xsl:variable name="item2" select="ec:Item(sc:fld('Product2',.)) "/>
    <div class="content4">
      <xsl:call-template name="column">
        <xsl:with-param name="idx" select="1"/>
        <xsl:with-param name="columns" select="4"/>
        <xsl:with-param name="item" select="ec:Item(sc:fld('Product1',.))"/>
      </xsl:call-template>
      <!-- col -->
      <xsl:call-template name="column">
        <xsl:with-param name="idx" select="2"/>
        <xsl:with-param name="columns" select="4"/>
        <xsl:with-param name="item" select="ec:Item(sc:fld('Product2',.))"/>
      </xsl:call-template>
      <!-- col -->
      <xsl:call-template name="column">
        <xsl:with-param name="idx" select="3"/>
        <xsl:with-param name="columns" select="4"/>
        <xsl:with-param name="item" select="ec:Item(sc:fld('Product3',.))"/>
      </xsl:call-template>
      <!-- col -->
      <xsl:call-template name="column">
        <xsl:with-param name="idx" select="4"/>
        <xsl:with-param name="columns" select="4"/>
        <xsl:with-param name="item" select="ec:Item(sc:fld('Product4',.))"/>
      </xsl:call-template>
      <!-- col -->

      <div class="clear"></div>
    </div>
  </xsl:template>
</xsl:stylesheet>