﻿<?xml version="1.0" encoding="UTF-8"?>

<!--=============================================================
    File: mcProsessLinje.xslt                                                   
    Created by: sitecore\admin                                       
    Created: 13.08.2008 18:35:05                                               
    Copyright notice at bottom of file
==============================================================-->

<xsl:stylesheet version="1.0"
  xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
  xmlns:sc="http://www.sitecore.net/sc"
  xmlns:dot="http://www.sitecore.net/dot"
  exclude-result-prefixes="dot sc">


  <!-- output directives -->
  <xsl:output method="xml" indent="yes" encoding="utf-8" omit-xml-declaration="yes"/>

  <!-- parameters -->
  <xsl:param name="lang" select="'en'"/>
  <xsl:param name="id" select="''"/>
  <xsl:param name="sc_item"/>
  <xsl:param name="sc_currentitem"/>

  <!-- include filer -->


  <!-- variables -->
  <xsl:variable name="home" select="$sc_item/ancestor-or-self::item[@template='site root']" />

  <!-- entry point -->
  <xsl:template match="*">
    <xsl:apply-templates select="$sc_item" mode="main"/>
  </xsl:template>

  <!--==============================================================-->
  <!-- main                                                         -->
  <!--==============================================================-->
  <xsl:template match="*" mode="main">
    <div class="checkoutHeader">      
        <xsl:if test="./../item[contains(@template, 'process line folder')]">
          <xsl:for-each select="./../item[contains(@template, 'process line folder')]/*">
            <xsl:choose>
              <xsl:when test="contains(sc:fld('chainedpage',.), $sc_item/@id)">                
                  <sc:image ID="headerImage" select="." field="Header Image" />                
              </xsl:when>                           
            </xsl:choose>
          </xsl:for-each>
        </xsl:if>
      </div>   
  </xsl:template>
</xsl:stylesheet>