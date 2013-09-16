<?xml version="1.0" encoding="UTF-8"?>

<!--=============================================================
    File: Rich Text.xslt                                                   
    Created by: sitecore\admin                                       
    Created: 07.12.2009 16:27:47                                               
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

  <!-- variables -->
  <xsl:variable name="OneColWidth" select="ec:GetDesignSetting('One Column Width')"/>
  <xsl:variable name="TwoColWidth" select="ec:GetDesignSetting('Two Column Width')"/>
  <xsl:variable name="ThreeColWidth" select="ec:GetDesignSetting('Three Column Width')"/>
  <xsl:variable name="FourColWidth" select="ec:GetDesignSetting('Four Column Width')"/>


  <xsl:variable name="TwoColHeight" select="ec:GetDesignSetting('Two Column Height')"/>
  <xsl:variable name="ThreeColHeight" select="ec:GetDesignSetting('Three Column Height')"/>
  <xsl:variable name="FourColHeight" select="ec:GetDesignSetting('Four Column Height')"/>
  
  <xsl:variable name="bgColor" select="ec:GetDesignSetting('Product Image Background Color')"/>
  
</xsl:stylesheet>
