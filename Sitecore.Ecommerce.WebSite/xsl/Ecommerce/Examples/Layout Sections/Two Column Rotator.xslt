﻿<?xml version="1.0" encoding="UTF-8"?>

<!--=============================================================
    File: Two Column Rotator.xslt                                                   
    Created by: sitecore\admin                                       
    Created: 07.12.2009 20:16:23                                               
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
  <xsl:param name="lang" />
  <xsl:param name="id" />
  <xsl:param name="sc_item"/>
  <xsl:param name="sc_currentitem"/>
  <xsl:param name="itemID"/>

  <!-- include fiels-->
  <xsl:include href="Rotator Spot.xslt"/>
  
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
      Column template - Two column Rotator
  =============================================-->
  <xsl:template match="*[@template='two column rotator']" mode="layout-section">
    <xsl:variable name="timeOutFldName1" select="'Rotate Frequency1'"/>
    <xsl:variable name="timeOutFldName2" select="'Rotate Frequency2'"/>
    <xsl:variable name="colWidth" select="310"/>


    <xsl:variable name="identifier" select="translate(@name,' ','_')"/>

    <div class="content2">
      <div class="colRotator">
        <xsl:if test="ec:IsEditingMode()">
          <div class="editMode">
            <span>
              <xsl:value-of select="ec:FieldTitle(@id,$timeOutFldName1)"/>:
              <span class="editText">
                <xsl:value-of select="sc:field($timeOutFldName1,.)" disable-output-escaping="yes"/>
              </span >
            </span>
          </div>
        </xsl:if>


        <xsl:call-template name="RotatorSpot" >
          <xsl:with-param name="ids" select="concat(sc:fld('Items1',.),'|')"/>
          <xsl:with-param name="idx" select="0"/>
          <xsl:with-param name="identifier" select="concat($identifier, '1')"/>
          <xsl:with-param name="width" select="$colWidth"/>
          <xsl:with-param name="timeout" select="sc:field($timeOutFldName1,.)"/>
        </xsl:call-template>
      </div>


      <div class="colRotator">
        <xsl:if test="ec:IsEditingMode()">
          <div class="editMode">
            <span>
              <xsl:value-of select="ec:FieldTitle(@id,$timeOutFldName2)"/>:
              <span class="editText">
                <xsl:value-of select="sc:field($timeOutFldName2,.)" disable-output-escaping="yes"/>
              </span >
            </span>
          </div>
        </xsl:if>


        <xsl:call-template name="RotatorSpot">
          <xsl:with-param name="ids" select="concat(sc:fld('Items2',.),'|')"/>
          <xsl:with-param name="idx" select="0"/>
          <xsl:with-param name="identifier" select="concat($identifier, '2')"/>
          <xsl:with-param name="width" select="$colWidth"/>
          <xsl:with-param name="timeout" select="sc:field($timeOutFldName2,.)"/>
        </xsl:call-template>

      </div>
      <div class="clear"></div>
    </div>
  </xsl:template>

</xsl:stylesheet>