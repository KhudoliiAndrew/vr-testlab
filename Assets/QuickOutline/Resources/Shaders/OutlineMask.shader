﻿//
//  OutlineMask.shader
//  QuickOutline
//
//  Created by Chris Nolet on 2/21/18.
//  Copyright © 2018 Chris Nolet. All rights reserved.
//

Shader "Unlit/Outline Mask"
{
    Properties
    {
        [Enum(UnityEngine.Rendering.CompareFunction)] _ZTest("ZTest", Float) = 0
    }

    SubShader
    {
        Tags
        {
            "Queue" = "0"
            "RenderType" = "Transparent"
        }
        LOD 100

        Pass
        {
            Name "Mask"
            Cull Off
            ZTest Always
            ZWrite Off
            ColorMask 0

            Stencil
            {
                Ref 1
                Pass Replace
            }
            
        }
    }
}