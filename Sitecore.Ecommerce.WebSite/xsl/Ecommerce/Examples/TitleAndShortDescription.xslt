﻿<?xml version="1.0" encoding="UTF-8"?>

<!--=============================================================
    File: breadcrumb.xslt                                                   
    Created by: sitecore\admin                                       
    Created: 13.08.2008 18:20:59                                               
    Copyright notice at bottom of file
==============================================================-->

<xsl:stylesheet version="1.0"
  xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
  xmlns:sc="http://www.sitecore.net/sc"
  xmlns:dot="http://www.sitecore.net/dot"
  xmlns:ec="http://www.sitecore.net/ec"
  exclude-result-prefixes="dot sc ec">

  <!-- output directives -->
  <xsl:output method="xml" indent="yes" encoding="utf-8" omit-xml-declaration="yes"/>

  <!-- parameters -->
  <xsl:param name="lang" select="'en'"/>
  <xsl:param name="id" select="''"/>
  <xsl:param name="sc_item"/>
  <xsl:param name="sc_currentitem"/>

  <!-- variables -->
  <xsl:variable name="home" select="ec:GetSiteStartItem()" />
  <xsl:variable name="seperator" select="'/'"/>

  <!-- entry point -->
  <xsl:template match="*">
    <xsl:apply-templates select="$sc_item" mode="main"/>
  </xsl:template>

  <!--==============================================================-->
  <!-- main                                                         -->
  <!--==============================================================-->
  <xsl:template match="*" mode="main">
    <div class="content">
      <div id="pb_header_shaddow">
        <h1>
          <sc:text field="Title" />
        </h1>
        <div class="clear">
        </div>
      </div>
      <xsl:if test="sc:fld('Short Description',.) !=''">
        <p>
          <sc:text field="Short Description" />
        </p>
      </xsl:if>

    </div>
  </xsl:template>
</xsl:stylesheet>