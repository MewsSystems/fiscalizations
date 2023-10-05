﻿using Mews.Fiscalizations.Basque.Dto.Bizkaia;

namespace Mews.Fiscalizations.Basque.Tests
{
    internal static class BatuzInvoiceRequestHelper
    {
        internal static LROEPJ240FacturasEmitidasConSGAltaPeticion CreateSampleBatuzRequest()
        {
            return new LROEPJ240FacturasEmitidasConSGAltaPeticion
            {
                Cabecera = new Cabecera2
                {
                    Modelo = Modelo240Enum.Item240,
                    Capitulo = CapituloModelo240Enum.Item1,
                    Subcapitulo = SubcapituloModelo240Enum.Item11,
                    Operacion = OperacionEnum.A00,
                    Version = IDVersionEnum.Item10,
                    Ejercicio = 2022,
                    SubcapituloSpecified = true,
                    ObligadoTributario = new NIFPersonaType
                    {
                        NIF = "B00000034",
                        ApellidosNombreRazonSocial = "HOTEL ADIBIDEZ"
                    },
                },
                FacturasEmitidas = new FacturaEmitidaType[]
                {
                    new FacturaEmitidaType
                    {
                        TicketBai = "PD94bWwgdmVyc2lvbj0iMS4wIiBlbmNvZGluZz0iVVRGLTgiPz48VDpUaWNrZXRCYWkgeG1sbnM6VD0idXJuOnRpY2tldGJhaTplbWlzaW9uIj4KCTxDYWJlY2VyYT4KCQk8SURWZXJzaW9uVEJBST4xLjI8L0lEVmVyc2lvblRCQUk+Cgk8L0NhYmVjZXJhPgogICAgPFN1amV0b3M+CiAgICAgICAgPEVtaXNvcj4KICAgICAgICAgICAgPE5JRj5CMDAwMDAwMzQ8L05JRj4KICAgICAgICAgICAgPEFwZWxsaWRvc05vbWJyZVJhem9uU29jaWFsPkhPVEVMIEFESUJJREVaPC9BcGVsbGlkb3NOb21icmVSYXpvblNvY2lhbD4KICAgICAgICA8L0VtaXNvcj4KICAgICAgICA8RGVzdGluYXRhcmlvcz4KICAgICAgICAgICAgPElERGVzdGluYXRhcmlvPgogICAgICAgICAgICAgICAgPE5JRj5CMjYyNDgxNDY8L05JRj4KICAgICAgICAgICAgICAgIDxBcGVsbGlkb3NOb21icmVSYXpvblNvY2lhbD5FTVBSRVNBIExBTlRFR0lBPC9BcGVsbGlkb3NOb21icmVSYXpvblNvY2lhbD4gICAgICAgICAgICAgICAgCiAgICAgICAgICAgIDwvSUREZXN0aW5hdGFyaW8+CiAgICAgICAgPC9EZXN0aW5hdGFyaW9zPgogICAgPC9TdWpldG9zPgogICAgPEZhY3R1cmE+CiAgICAgICAgPENhYmVjZXJhRmFjdHVyYT4KICAgICAgICAgICAgPFNlcmllRmFjdHVyYT5CMjAyMjwvU2VyaWVGYWN0dXJhPgogICAgICAgICAgICA8TnVtRmFjdHVyYT4wMTAwPC9OdW1GYWN0dXJhPgogICAgICAgICAgICA8RmVjaGFFeHBlZGljaW9uRmFjdHVyYT4zMC0wMS0yMDIyPC9GZWNoYUV4cGVkaWNpb25GYWN0dXJhPgogICAgICAgICAgICA8SG9yYUV4cGVkaWNpb25GYWN0dXJhPjE4OjAwOjE3PC9Ib3JhRXhwZWRpY2lvbkZhY3R1cmE+CiAgICAgICAgPC9DYWJlY2VyYUZhY3R1cmE+CiAgICAgICAgPERhdG9zRmFjdHVyYT4KICAgICAgICAgICAgPERlc2NyaXBjaW9uRmFjdHVyYT5TZXJ2aWNpb3MgSG90ZWw8L0Rlc2NyaXBjaW9uRmFjdHVyYT4KICAgICAgICAgICAgPEltcG9ydGVUb3RhbEZhY3R1cmE+MjM0My4wMDwvSW1wb3J0ZVRvdGFsRmFjdHVyYT4KICAgICAgICAgICAgPENsYXZlcz4KICAgICAgICAgICAgICAgIDxJRENsYXZlPgogICAgICAgICAgICAgICAgICAgIDxDbGF2ZVJlZ2ltZW5JdmFPcFRyYXNjZW5kZW5jaWE+MDE8L0NsYXZlUmVnaW1lbkl2YU9wVHJhc2NlbmRlbmNpYT4KICAgICAgICAgICAgICAgIDwvSURDbGF2ZT4KICAgICAgICAgICAgPC9DbGF2ZXM+CiAgICAgICAgPC9EYXRvc0ZhY3R1cmE+CiAgICAgICAgPFRpcG9EZXNnbG9zZT4KICAgICAgICAgICAgPERlc2dsb3NlRmFjdHVyYT4KICAgICAgICAgICAgICAgIDxTdWpldGE+CiAgICAgICAgICAgICAgICAgICAgPE5vRXhlbnRhPgoJCQkJCQk8RGV0YWxsZU5vRXhlbnRhPgoJCQkJCQkJPFRpcG9Ob0V4ZW50YT5TMTwvVGlwb05vRXhlbnRhPgoJCQkJCQkJPERlc2dsb3NlSVZBPgoJCQkJCQkJCTxEZXRhbGxlSVZBPgoJCQkJCQkJCQk8QmFzZUltcG9uaWJsZT4zMDAuMDA8L0Jhc2VJbXBvbmlibGU+CgkJCQkJCQkJCTxUaXBvSW1wb3NpdGl2bz4yMS4wMDwvVGlwb0ltcG9zaXRpdm8+CgkJCQkJCQkJCTxDdW90YUltcHVlc3RvPjYzLjAwPC9DdW90YUltcHVlc3RvPgoJCQkJCQkJCTwvRGV0YWxsZUlWQT4KCQkJCQkJCQk8RGV0YWxsZUlWQT4KCQkJCQkJCQkJPEJhc2VJbXBvbmlibGU+MTgwMC4wMDwvQmFzZUltcG9uaWJsZT4KCQkJCQkJCQkJPFRpcG9JbXBvc2l0aXZvPjEwLjAwPC9UaXBvSW1wb3NpdGl2bz4KCQkJCQkJCQkJPEN1b3RhSW1wdWVzdG8+MTgwLjAwPC9DdW90YUltcHVlc3RvPgoJCQkJCQkJCTwvRGV0YWxsZUlWQT4JCQkJCQkJCQoJCQkJCQkJPC9EZXNnbG9zZUlWQT4KCQkJCQkJPC9EZXRhbGxlTm9FeGVudGE+CiAgICAgICAgICAgICAgICAgICAgPC9Ob0V4ZW50YT4KICAgICAgICAgICAgICAgIDwvU3VqZXRhPgogICAgICAgICAgICA8L0Rlc2dsb3NlRmFjdHVyYT4KICAgICAgICA8L1RpcG9EZXNnbG9zZT4KICAgIDwvRmFjdHVyYT4KICAgIDxIdWVsbGFUQkFJPgoJPEVuY2FkZW5hbWllbnRvRmFjdHVyYUFudGVyaW9yPgoJCTxTZXJpZUZhY3R1cmFBbnRlcmlvcj5CMjAyMjwvU2VyaWVGYWN0dXJhQW50ZXJpb3I+CgkJPE51bUZhY3R1cmFBbnRlcmlvcj4wMDk5PC9OdW1GYWN0dXJhQW50ZXJpb3I+CgkJPEZlY2hhRXhwZWRpY2lvbkZhY3R1cmFBbnRlcmlvcj4yOS0wMS0yMDIyPC9GZWNoYUV4cGVkaWNpb25GYWN0dXJhQW50ZXJpb3I+CgkJPFNpZ25hdHVyZVZhbHVlRmlybWFGYWN0dXJhQW50ZXJpb3I+QmVNa0t3WGFGc3hIUWVjNjVTS3BWUDdFVTlvNG5VWE94N1NBZnRJVG9Gc3hIKzJqMnRYUFhocEJVblMyNmRoZFNwaU1sMkRsVHVxUnNGZFpmV3lZYXphR0hnU1JRSFpaQW5GdDwvU2lnbmF0dXJlVmFsdWVGaXJtYUZhY3R1cmFBbnRlcmlvcj4KCTwvRW5jYWRlbmFtaWVudG9GYWN0dXJhQW50ZXJpb3I+Cgk8U29mdHdhcmU+CgkgICAgPExpY2VuY2lhVEJBST5UQkFJUFJVRUJBPC9MaWNlbmNpYVRCQUk+CgkgICAgPEVudGlkYWREZXNhcnJvbGxhZG9yYT4KICAgICAgICAJPE5JRj5BNDgxMTk4MjA8L05JRj4KCSAgICA8L0VudGlkYWREZXNhcnJvbGxhZG9yYT4KICAgICAgICAgICAgPE5vbWJyZT5ERkJUQkFJPC9Ob21icmU+CiAgICAgICAgICAgIDxWZXJzaW9uPjEuMDQuMDA8L1ZlcnNpb24+CiAgICAgICAgPC9Tb2Z0d2FyZT4KICAgICAgICA8TnVtU2VyaWVEaXNwb3NpdGl2bz5HUDRGQzVKPC9OdW1TZXJpZURpc3Bvc2l0aXZvPgogICAgPC9IdWVsbGFUQkFJPgo8ZHM6U2lnbmF0dXJlIHhtbG5zOmRzPSJodHRwOi8vd3d3LnczLm9yZy8yMDAwLzA5L3htbGRzaWcjIiBJZD0iU2lnbmF0dXJlLWE1M2E2YWIyLWY5MDQtNGY3Yy1iZTY0LTYwMzMzM2Y2NTFiZi1TaWduYXR1cmUiPjxkczpTaWduZWRJbmZvPjxkczpDYW5vbmljYWxpemF0aW9uTWV0aG9kIEFsZ29yaXRobT0iaHR0cDovL3d3dy53My5vcmcvVFIvMjAwMS9SRUMteG1sLWMxNG4tMjAwMTAzMTUiLz48ZHM6U2lnbmF0dXJlTWV0aG9kIEFsZ29yaXRobT0iaHR0cDovL3d3dy53My5vcmcvMjAwMS8wNC94bWxkc2lnLW1vcmUjcnNhLXNoYTI1NiIvPjxkczpSZWZlcmVuY2UgSWQ9IlJlZmVyZW5jZS1jZjU0ZTZhOS03YmYyLTQxMjgtOGNhZS00NzRhMjY3YTE2YTEiIFR5cGU9Imh0dHA6Ly93d3cudzMub3JnLzIwMDAvMDkveG1sZHNpZyNPYmplY3QiIFVSST0iIj48ZHM6VHJhbnNmb3Jtcz48ZHM6VHJhbnNmb3JtIEFsZ29yaXRobT0iaHR0cDovL3d3dy53My5vcmcvVFIvMjAwMS9SRUMteG1sLWMxNG4tMjAwMTAzMTUiLz48ZHM6VHJhbnNmb3JtIEFsZ29yaXRobT0iaHR0cDovL3d3dy53My5vcmcvMjAwMC8wOS94bWxkc2lnI2VudmVsb3BlZC1zaWduYXR1cmUiLz48ZHM6VHJhbnNmb3JtIEFsZ29yaXRobT0iaHR0cDovL3d3dy53My5vcmcvVFIvMTk5OS9SRUMteHBhdGgtMTk5OTExMTYiPjxkczpYUGF0aCB4bWxuczpkcz0iaHR0cDovL3d3dy53My5vcmcvMjAwMC8wOS94bWxkc2lnIyI+bm90KGFuY2VzdG9yLW9yLXNlbGY6OmRzOlNpZ25hdHVyZSk8L2RzOlhQYXRoPjwvZHM6VHJhbnNmb3JtPjwvZHM6VHJhbnNmb3Jtcz48ZHM6RGlnZXN0TWV0aG9kIEFsZ29yaXRobT0iaHR0cDovL3d3dy53My5vcmcvMjAwMS8wNC94bWxlbmMjc2hhNTEyIi8+PGRzOkRpZ2VzdFZhbHVlPmlVMGI1UjRTMVdRbEpnek1VVzc0WG1VakR4dURtU1V2R1FVZlZnRHJOdmtxb1JUblczdGdiWUpoWUswUEhUZVJpRkJNRWwxemQ5dnF3bks5TzAwcjVBPT08L2RzOkRpZ2VzdFZhbHVlPjwvZHM6UmVmZXJlbmNlPjxkczpSZWZlcmVuY2UgVHlwZT0iaHR0cDovL3VyaS5ldHNpLm9yZy8wMTkwMyNTaWduZWRQcm9wZXJ0aWVzIiBVUkk9IiNTaWduYXR1cmUtYTUzYTZhYjItZjkwNC00ZjdjLWJlNjQtNjAzMzMzZjY1MWJmLVNpZ25lZFByb3BlcnRpZXMiPjxkczpEaWdlc3RNZXRob2QgQWxnb3JpdGhtPSJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGVuYyNzaGE1MTIiLz48ZHM6RGlnZXN0VmFsdWU+K3RWcE1vZExEL0VDY3Q4djBIQ3hZeitVdUFQNVFVMUdmaVBoUVU4NVNnbnNjbmozT3VMekxnNzRDLzhHOUV0UzlxemFCYnl5cjNlVHgvenU1L1Nocnc9PTwvZHM6RGlnZXN0VmFsdWU+PC9kczpSZWZlcmVuY2U+PGRzOlJlZmVyZW5jZSBVUkk9IiNTaWduYXR1cmUtYTUzYTZhYjItZjkwNC00ZjdjLWJlNjQtNjAzMzMzZjY1MWJmLUtleUluZm8iPjxkczpEaWdlc3RNZXRob2QgQWxnb3JpdGhtPSJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGVuYyNzaGE1MTIiLz48ZHM6RGlnZXN0VmFsdWU+OVB3VDJhWFc2V2E4MXB3bFptUWp2dUp6eTNhL0hKNmdVdWFKR0pHQjBaUUdwTXN2WXVKNWltbFpnTnRnbGRHOUV2WDBDSmlvUnM5S0lpYTNXeUY2aWc9PTwvZHM6RGlnZXN0VmFsdWU+PC9kczpSZWZlcmVuY2U+PC9kczpTaWduZWRJbmZvPjxkczpTaWduYXR1cmVWYWx1ZSBJZD0iU2lnbmF0dXJlLWE1M2E2YWIyLWY5MDQtNGY3Yy1iZTY0LTYwMzMzM2Y2NTFiZi1TaWduYXR1cmVWYWx1ZSI+RFNJMVhBMUVyQ29xYUVsbisrMjE2ZVRRcE4wcUY3SFFiL0kzUWhGNTRWMTFMRUdZM3VjTVZIMzNHY2VSRTlMdFlKSTdBb0kwWVMzcTBxUE9uK2VUbEg2WkhWWFhrY1ZaeWkyZ0JaY3VDS3FSaVJ6U2l1UzBNYXpEZ1lxQmRyRFdoQUlCeVA2QUoydlJoeGhaY2VBQ2VIRk56Zm1qZURwalgwMWNVb3gvUkZULy9DNGNVT3VUV0FEbGtxL1BxRU4wZEsvVVFiWkVPc3NrNGRpSWxDZVlVL2V4VllvUklwa0U5T2NGcmx3T0p2L1VKM2V6QmxMOXBkLzZHNXJoTk10d3FuUFhMa2JCcTk5M3V1VkNVMlNlS2k3OUh1QWEvZ0N2SGxGd3JqaHdKVjZWZ2NHaFB5bEo2N3hKazZkcSt1a1NXMnM2a0k0T2dhdUg0U3pZVWFJQ0FRPT08L2RzOlNpZ25hdHVyZVZhbHVlPjxkczpLZXlJbmZvIElkPSJTaWduYXR1cmUtYTUzYTZhYjItZjkwNC00ZjdjLWJlNjQtNjAzMzMzZjY1MWJmLUtleUluZm8iPjxkczpYNTA5RGF0YT48ZHM6WDUwOUNlcnRpZmljYXRlPk1JSUhzVENDQlptZ0F3SUJBZ0lRS3AwT2F3eHp0bWhiN2FVd1VrWFBxakFOQmdrcWhraUc5dzBCQVFzRkFEQ0JpVEVMTUFrR0ExVUVCaE1DUlZNeEZEQVNCZ05WQkFvTUMwbGFSVTVRUlNCVExrRXVNVG93T0FZRFZRUUxEREZCV2xvZ1dtbDFjblJoWjJseWFTQndkV0pzYVd0dllTQXRJRU5sY25ScFptbGpZV1J2SUhCMVlteHBZMjhnVTBOQk1TZ3dKZ1lEVlFRRERCOURRU0JCUVZCUUlGWmhjMk5oY3lBb01pa2dMU0JFUlZOQlVsSlBURXhQTUI0WERURTRNVEV4TlRFMk5UWXhObG9YRFRNM01URXlPREl6TURBd01Gb3dkakVMTUFrR0ExVUVCaE1DUlZNeER6QU5CZ05WQkFvTUJreEJUbFJKU3pFM01EVUdBMVVFQ3d3dVIyRnBiSFVnZW1sMWNuUmhaMmx5YVdFZ0xTQkRaWEowYVdacFkyRmtieUJrWlNCa2FYTndiM05wZEdsMmJ6RUxNQWtHQTFVRUN3d0NVRU14RURBT0JnTlZCQU1NQjBkUU5FWkROVW93Z2dFaU1BMEdDU3FHU0liM0RRRUJBUVVBQTRJQkR3QXdnZ0VLQW9JQkFRQzVmVUtoM1ZkdUFBRmgvOVhLL3pIa2Vob21ubU9maXB3QWk4WnBkdE12NHhXUk1ERFc1WXdKdXZBK2tDRHhwUmE5MDVpc09UNnhuVS9zU0REOHVZT242L1JXRzFTV2YwdDhhRXFrMGtZZkN2UVRvaGIxYlVlY0pOS1pXdVoyb3BSemhHUHpLenMvUjB5amJhSUk3UnNFY2t5em5rRHNGQ3NFamE2c3FPWVg5NlY3dTJvdlpQZk15aEV2QWIwb0J1YklOd3BxdFFPZW5tQ0YzSVZDMFVYT3dUNHQ1aWpHV3NmUlFlRzEyU3JOTGZrZ3FmZXdERTNJSGREQVJMTEFSQmt4RUs1dGhVQjhSTFZTRWlza3ZMdGFzLy9kVlMzUWd2ZUpLUmgwZ3ZNdHpNWkRtUTNQQ21paEtObUlsMXB0WTdpK2oxNlNFTGRoQzVKUVVoaEM2alNKQWdNQkFBR2pnZ01sTUlJRElUQ0J4d1lEVlIwU0JJRy9NSUc4aGhWb2RIUndPaTh2ZDNkM0xtbDZaVzV3WlM1amIyMkJEMmx1Wm05QWFYcGxibkJsTG1OdmJhU0JrVENCampGSE1FVUdBMVVFQ2d3K1NWcEZUbEJGSUZNdVFTNGdMU0JEU1VZZ1FUQXhNek0zTWpZd0xWSk5aWEpqTGxacGRHOXlhV0V0UjJGemRHVnBlaUJVTVRBMU5TQkdOaklnVXpneFF6QkJCZ05WQkFrTU9rRjJaR0VnWkdWc0lFMWxaR2wwWlhKeVlXNWxieUJGZEc5eVltbGtaV0VnTVRRZ0xTQXdNVEF4TUNCV2FYUnZjbWxoTFVkaGMzUmxhWG93RGdZRFZSMFBBUUgvQkFRREFnV2dNQjBHQTFVZERnUVdCQlJlaUNjLzhxOEJVM0ZHcWNGS2xuNzFpb2hzTlRBZkJnTlZIU01FR0RBV2dCVEN2SmZ0MjdvUTNTeE0rSlBHNzA0ekJra2Y5ekNDQVI0R0ExVWRJQVNDQVJVd2dnRVJNSUlCRFFZS0t3WUJCQUh6T1dVREFqQ0IvakFsQmdnckJnRUZCUWNDQVJZWmFIUjBjRG92TDNkM2R5NXBlbVZ1Y0dVdVkyOXRMMk53Y3pDQjFBWUlLd1lCQlFVSEFnSXdnY2NNZ2NSQ1pYSnRaV1Z1SUcxMVoyRnJJR1Y2WVdkMWRIcGxhMjhnZDNkM0xtbDZaVzV3WlM1amIyMGdXbWwxY25SaFoybHlhV0Z1SUd0dmJtWnBZVzUwZW1FZ2FYcGhiaUJoZFhKeVpYUnBheUJyYjI1MGNtRjBkV0VnYVhKaGEzVnljbWt1VEdsdGFYUmhZMmx2Ym1WeklHUmxJR2RoY21GdWRHbGhjeUJsYmlCM2QzY3VhWHBsYm5CbExtTnZiU0JEYjI1emRXeDBaU0JsYkNCamIyNTBjbUYwYnlCaGJuUmxjeUJrWlNCamIyNW1hV0Z5SUdWdUlHVnNJR05sY25ScFptbGpZV1J2TUlHakJnZ3JCZ0VGQlFjQkFRU0JsakNCa3pBbEJnZ3JCZ0VGQlFjd0FZWVphSFIwY0RvdkwyOWpjM0JrWlhNdWFYcGxibkJsTG1OdmJUQnFCZ2dyQmdFRkJRY3dBb1plYUhSMGNEb3ZMM2QzZHk1cGVtVnVjR1V1WTI5dEwyTnZiblJsYm1sa2IzTXZhVzVtYjNKdFlXTnBiMjR2WTJGelgybDZaVzV3WlM5bGMxOWpZWE12WVdScWRXNTBiM012UVVGUVVFNVNYMk5sY25SZmMyaGhNalUyTG1OeWREQTlCZ05WSFI4RU5qQTBNREtnTUtBdWhpeG9kSFJ3T2k4dlkzSnNaR1Z6TG1sNlpXNXdaUzVqYjIwdlkyZHBMV0pwYmk5amNteHBiblJsY201aE1qQU5CZ2txaGtpRzl3MEJBUXNGQUFPQ0FnRUFZYVBGVTFuOVArVEhuQ1M0TThFL3hiNThocXBoNUJEZ3hyRmJCUFlRNEQ4YUpPYVNFcWZQUHJoTzlmbFlURlNjbkdjMWlOZGpaYmpRZENNb2Z6T2xRUzNuVXlHRFJvT3htSW9ReS9ack95SElhaVlGaG9ZYWtzNXdVa3ZWUmU0OFpqSGZlMGxiZ1lZcHJMN2FxcnRrNnNJd2JjZkMwMjk5WFVpTmVjRWRLWmErV2NvY21HOUxYVFE0OXNkRThlS0UwSEVLT1JsUFZxNU1EWERJbjRQeWkwTlBlZmo0V2VQM3BPeW01cGppWDJGeGExcDB1cS9hN054TVRIL1VCQVZHMzhWWlMwNk5zNUQvaER1YXZ0aXUyb1lMcnNnQlpCUER5dzBlOGxidmYzeXptZjMrazBjUVhwMEV0VjhTOVVmMUl3WFNwTVB2TWFNemViUFFrK1c2NEFsQlVUbnprdExkb040UTRiVERiNzEyTUdPdjRQM2I1T1Z2RWtUQ3VtVGZBM3U1dXNGdCszdnk3em5GZ2g3NS9HWW9mc2tCN3JKREpFbTcwaG9vQ0M0MllZMTVyL2JEczlUSTNzQlE3N1hXLzhHRHpVakQ3T2RkNXFSQmdsVjFOUG1seXZxSkhNTGtDcUkvZXloSW1QZXZ4ME9pVXhBMTBUZml6ZElMWWQwdEcxc0N1U0hwSmpiTzNBY0lWeTg5dXpFTEN4YjBRRU1EU1prcCtOWmRtWFFndjJ6VUJxU3BnNVNpSEdlVkx6a1I1WGJiM3dESC9zSFdFek1VUllyUE11UzB6L2pweTluWUJ6UStiUzE0dCtLSTlmZHlkbXlxbm8yMU51aTVDVEU5aHFCbjlpdVdhNlhocElIZ1huYUorc2JSM0h4QmcrdVRvSmZMVWhyRlJhQT08L2RzOlg1MDlDZXJ0aWZpY2F0ZT48L2RzOlg1MDlEYXRhPjxkczpLZXlWYWx1ZT48ZHM6UlNBS2V5VmFsdWU+PGRzOk1vZHVsdXM+dVgxQ29kMVhiZ0FCWWYvVnl2OHg1SG9hSnA1am40cWNBSXZHYVhiVEwrTVZrVEF3MXVXTUNicndQcEFnOGFVV3ZkT1lyRGsrc1oxUDdFZ3cvTG1EcCt2MFZodFVsbjlMZkdoS3BOSkdId3IwRTZJVzlXMUhuQ1RTbVZybWRxS1VjNFJqOHlzN1AwZE1vMjJpQ08wYkJISk1zNTVBN0JRckJJMnVyS2ptRi9lbGU3dHFMMlQzek1vUkx3RzlLQWJteURjS2FyVURucDVnaGR5RlF0RkZ6c0UrTGVZb3hsckgwVUhodGRrcXpTMzVJS24zc0F4TnlCM1F3RVN5d0VRWk1SQ3ViWVZBZkVTMVVoSXJKTHk3V3JQLzNWVXQwSUwzaVNrWWRJTHpMY3pHUTVrTnp3cG9vU2paaUpkYWJXTzR2bzlla2hDM1lRdVNVRklZUXVvMGlRPT08L2RzOk1vZHVsdXM+PGRzOkV4cG9uZW50PkFRQUI8L2RzOkV4cG9uZW50PjwvZHM6UlNBS2V5VmFsdWU+PC9kczpLZXlWYWx1ZT48L2RzOktleUluZm8+PGRzOk9iamVjdD48eGFkZXM6UXVhbGlmeWluZ1Byb3BlcnRpZXMgeG1sbnM6eGFkZXM9Imh0dHA6Ly91cmkuZXRzaS5vcmcvMDE5MDMvdjEuMy4yIyIgSWQ9IlNpZ25hdHVyZS1hNTNhNmFiMi1mOTA0LTRmN2MtYmU2NC02MDMzMzNmNjUxYmYtUXVhbGlmeWluZ1Byb3BlcnRpZXMiIFRhcmdldD0iI1NpZ25hdHVyZS1hNTNhNmFiMi1mOTA0LTRmN2MtYmU2NC02MDMzMzNmNjUxYmYtU2lnbmF0dXJlIiB4bWxuczpkcz0iaHR0cDovL3d3dy53My5vcmcvMjAwMC8wOS94bWxkc2lnIyI+PHhhZGVzOlNpZ25lZFByb3BlcnRpZXMgSWQ9IlNpZ25hdHVyZS1hNTNhNmFiMi1mOTA0LTRmN2MtYmU2NC02MDMzMzNmNjUxYmYtU2lnbmVkUHJvcGVydGllcyI+PHhhZGVzOlNpZ25lZFNpZ25hdHVyZVByb3BlcnRpZXM+PHhhZGVzOlNpZ25pbmdUaW1lPjIwMjAtMTAtMDJUMTQ6NTQ6NTArMDI6MDA8L3hhZGVzOlNpZ25pbmdUaW1lPjx4YWRlczpTaWduaW5nQ2VydGlmaWNhdGU+PHhhZGVzOkNlcnQ+PHhhZGVzOkNlcnREaWdlc3Q+PGRzOkRpZ2VzdE1ldGhvZCBBbGdvcml0aG09Imh0dHA6Ly93d3cudzMub3JnLzIwMDEvMDQveG1sZW5jI3NoYTUxMiIvPjxkczpEaWdlc3RWYWx1ZT4raUpvbGxJZjExRCtBOS9tQXpPVU02U1NKdkZQT25lb09uN05JS2YrTnFrcGNFN1ZVTXg0eGlHancwRDhKTnJmcmV4Snd4bWxNVFdkM0VnL2QzQnEyUT09PC9kczpEaWdlc3RWYWx1ZT48L3hhZGVzOkNlcnREaWdlc3Q+PHhhZGVzOklzc3VlclNlcmlhbD48ZHM6WDUwOUlzc3Vlck5hbWU+Q049Q0EgQUFQUCBWYXNjYXMgKDIpIC0gREVTQVJST0xMTywgT1U9QVpaIFppdXJ0YWdpcmkgcHVibGlrb2EgLSBDZXJ0aWZpY2FkbyBwdWJsaWNvIFNDQSwgTz1JWkVOUEUgUy5BLiwgQz1FUzwvZHM6WDUwOUlzc3Vlck5hbWU+PGRzOlg1MDlTZXJpYWxOdW1iZXI+NTY2NDMwNTg4NjQ3NTc5ODI3MzIyMDY0NjM2MDEwODI3NDg4NDI8L2RzOlg1MDlTZXJpYWxOdW1iZXI+PC94YWRlczpJc3N1ZXJTZXJpYWw+PC94YWRlczpDZXJ0PjwveGFkZXM6U2lnbmluZ0NlcnRpZmljYXRlPjx4YWRlczpTaWduYXR1cmVQb2xpY3lJZGVudGlmaWVyPjx4YWRlczpTaWduYXR1cmVQb2xpY3lJZD48eGFkZXM6U2lnUG9saWN5SWQ+PHhhZGVzOklkZW50aWZpZXI+aHR0cHM6Ly93d3cuYmF0dXouZXVzL2ZpdHhhdGVnaWFrL2JhdHV6L3RpY2tldGJhaS9zaW5hZHVyYV9lbGVrdHJvbmlrb2FyZW5femVoYXp0YXBlbmFrX2VzcGVjaWZpY2FjaW9uZXNfZGVfbGFfZmlybWFfZWxlY3Ryb25pY2FfdjFfMC5wZGY8L3hhZGVzOklkZW50aWZpZXI+PHhhZGVzOkRlc2NyaXB0aW9uLz48L3hhZGVzOlNpZ1BvbGljeUlkPjx4YWRlczpTaWdQb2xpY3lIYXNoPjxkczpEaWdlc3RNZXRob2QgQWxnb3JpdGhtPSJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGVuYyNzaGEyNTYiLz48ZHM6RGlnZXN0VmFsdWU+UXV6bjk4eDNQTWJTSHdiVXphajVmNUtPcGlIMHU4YnZtd2JiYk5rTzlFcz08L2RzOkRpZ2VzdFZhbHVlPjwveGFkZXM6U2lnUG9saWN5SGFzaD48eGFkZXM6U2lnUG9saWN5UXVhbGlmaWVycz48eGFkZXM6U2lnUG9saWN5UXVhbGlmaWVyPjx4YWRlczpTUFVSST5odHRwczovL3d3dy5iYXR1ei5ldXMvZml0eGF0ZWdpYWsvYmF0dXovdGlja2V0YmFpL3NpbmFkdXJhX2VsZWt0cm9uaWtvYXJlbl96ZWhhenRhcGVuYWtfZXNwZWNpZmljYWNpb25lc19kZV9sYV9maXJtYV9lbGVjdHJvbmljYV92MV8wLnBkZjwveGFkZXM6U1BVUkk+PC94YWRlczpTaWdQb2xpY3lRdWFsaWZpZXI+PC94YWRlczpTaWdQb2xpY3lRdWFsaWZpZXJzPjwveGFkZXM6U2lnbmF0dXJlUG9saWN5SWQ+PC94YWRlczpTaWduYXR1cmVQb2xpY3lJZGVudGlmaWVyPjwveGFkZXM6U2lnbmVkU2lnbmF0dXJlUHJvcGVydGllcz48eGFkZXM6U2lnbmVkRGF0YU9iamVjdFByb3BlcnRpZXM+PHhhZGVzOkRhdGFPYmplY3RGb3JtYXQgT2JqZWN0UmVmZXJlbmNlPSIjUmVmZXJlbmNlLWNmNTRlNmE5LTdiZjItNDEyOC04Y2FlLTQ3NGEyNjdhMTZhMSI+PHhhZGVzOkRlc2NyaXB0aW9uLz48eGFkZXM6T2JqZWN0SWRlbnRpZmllcj48eGFkZXM6SWRlbnRpZmllciBRdWFsaWZpZXI9Ik9JREFzVVJOIj51cm46b2lkOjEuMi44NDAuMTAwMDMuNS4xMDkuMTA8L3hhZGVzOklkZW50aWZpZXI+PHhhZGVzOkRlc2NyaXB0aW9uLz48L3hhZGVzOk9iamVjdElkZW50aWZpZXI+PHhhZGVzOk1pbWVUeXBlPnRleHQveG1sPC94YWRlczpNaW1lVHlwZT48eGFkZXM6RW5jb2RpbmcvPjwveGFkZXM6RGF0YU9iamVjdEZvcm1hdD48L3hhZGVzOlNpZ25lZERhdGFPYmplY3RQcm9wZXJ0aWVzPjwveGFkZXM6U2lnbmVkUHJvcGVydGllcz48L3hhZGVzOlF1YWxpZnlpbmdQcm9wZXJ0aWVzPjwvZHM6T2JqZWN0PjwvZHM6U2lnbmF0dXJlPjwvVDpUaWNrZXRCYWk+"
                    },
                    new FacturaEmitidaType
                    {
                        TicketBai = "PD94bWwgdmVyc2lvbj0iMS4wIiBlbmNvZGluZz0iVVRGLTgiPz48VDpUaWNrZXRCYWkgeG1sbnM6VD0idXJuOnRpY2tldGJhaTplbWlzaW9uIj4KCTxDYWJlY2VyYT4KCQk8SURWZXJzaW9uVEJBST4xLjI8L0lEVmVyc2lvblRCQUk+Cgk8L0NhYmVjZXJhPgogICAgPFN1amV0b3M+CiAgICAgICAgPEVtaXNvcj4KICAgICAgICAgICAgPE5JRj5CMDAwMDAwMzQ8L05JRj4KICAgICAgICAgICAgPEFwZWxsaWRvc05vbWJyZVJhem9uU29jaWFsPkhPVEVMIEFESUJJREVaPC9BcGVsbGlkb3NOb21icmVSYXpvblNvY2lhbD4KICAgICAgICA8L0VtaXNvcj4KICAgICAgICA8RGVzdGluYXRhcmlvcz4KICAgICAgICAgICAgPElERGVzdGluYXRhcmlvPgogICAgICAgICAgICAgICAgPE5JRj4xMjM0NTY3OFo8L05JRj4KICAgICAgICAgICAgICAgIDxBcGVsbGlkb3NOb21icmVSYXpvblNvY2lhbD5ERSBFSkVNUExPIERFIEVKRU1QTE8gQU5BPC9BcGVsbGlkb3NOb21icmVSYXpvblNvY2lhbD4gICAgICAgICAgICAgICAgCiAgICAgICAgICAgIDwvSUREZXN0aW5hdGFyaW8+CiAgICAgICAgPC9EZXN0aW5hdGFyaW9zPgogICAgPC9TdWpldG9zPgogICAgPEZhY3R1cmE+CiAgICAgICAgPENhYmVjZXJhRmFjdHVyYT4KICAgICAgICAgICAgPFNlcmllRmFjdHVyYT5CMjAyMjwvU2VyaWVGYWN0dXJhPgogICAgICAgICAgICA8TnVtRmFjdHVyYT4wMTAxPC9OdW1GYWN0dXJhPgogICAgICAgICAgICA8RmVjaGFFeHBlZGljaW9uRmFjdHVyYT4wNS0wMi0yMDIyPC9GZWNoYUV4cGVkaWNpb25GYWN0dXJhPgogICAgICAgICAgICA8SG9yYUV4cGVkaWNpb25GYWN0dXJhPjExOjEwOjEzPC9Ib3JhRXhwZWRpY2lvbkZhY3R1cmE+CiAgICAgICAgPC9DYWJlY2VyYUZhY3R1cmE+CiAgICAgICAgPERhdG9zRmFjdHVyYT4KICAgICAgICAgICAgPERlc2NyaXBjaW9uRmFjdHVyYT5TZXJ2aWNpb3MgSG90ZWw8L0Rlc2NyaXBjaW9uRmFjdHVyYT4KICAgICAgICAgICAgPEltcG9ydGVUb3RhbEZhY3R1cmE+MTIxLjAwPC9JbXBvcnRlVG90YWxGYWN0dXJhPgogICAgICAgICAgICA8Q2xhdmVzPgogICAgICAgICAgICAgICAgPElEQ2xhdmU+CiAgICAgICAgICAgICAgICAgICAgPENsYXZlUmVnaW1lbkl2YU9wVHJhc2NlbmRlbmNpYT4wMTwvQ2xhdmVSZWdpbWVuSXZhT3BUcmFzY2VuZGVuY2lhPgogICAgICAgICAgICAgICAgPC9JRENsYXZlPgogICAgICAgICAgICA8L0NsYXZlcz4KICAgICAgICA8L0RhdG9zRmFjdHVyYT4KICAgICAgICA8VGlwb0Rlc2dsb3NlPgogICAgICAgICAgICA8RGVzZ2xvc2VGYWN0dXJhPgogICAgICAgICAgICAgICAgPFN1amV0YT4KICAgICAgICAgICAgICAgICAgICA8Tm9FeGVudGE+CgkJCQkJCTxEZXRhbGxlTm9FeGVudGE+CgkJCQkJCQk8VGlwb05vRXhlbnRhPlMxPC9UaXBvTm9FeGVudGE+CgkJCQkJCQk8RGVzZ2xvc2VJVkE+CgkJCQkJCQkJPERldGFsbGVJVkE+CgkJCQkJCQkJCTxCYXNlSW1wb25pYmxlPjExMC4wMDwvQmFzZUltcG9uaWJsZT4KCQkJCQkJCQkJPFRpcG9JbXBvc2l0aXZvPjEwLjAwPC9UaXBvSW1wb3NpdGl2bz4KCQkJCQkJCQkJPEN1b3RhSW1wdWVzdG8+MTEuMDA8L0N1b3RhSW1wdWVzdG8+CgkJCQkJCQkJPC9EZXRhbGxlSVZBPgoJCQkJCQkJPC9EZXNnbG9zZUlWQT4KCQkJCQkJPC9EZXRhbGxlTm9FeGVudGE+CiAgICAgICAgICAgICAgICAgICAgPC9Ob0V4ZW50YT4KICAgICAgICAgICAgICAgIDwvU3VqZXRhPgogICAgICAgICAgICA8L0Rlc2dsb3NlRmFjdHVyYT4KICAgICAgICA8L1RpcG9EZXNnbG9zZT4KICAgIDwvRmFjdHVyYT4KICAgIDxIdWVsbGFUQkFJPgoJPEVuY2FkZW5hbWllbnRvRmFjdHVyYUFudGVyaW9yPgoJCTxTZXJpZUZhY3R1cmFBbnRlcmlvcj5CMjAyMjwvU2VyaWVGYWN0dXJhQW50ZXJpb3I+CgkJPE51bUZhY3R1cmFBbnRlcmlvcj4wMTAwPC9OdW1GYWN0dXJhQW50ZXJpb3I+CgkJPEZlY2hhRXhwZWRpY2lvbkZhY3R1cmFBbnRlcmlvcj4zMC0wMS0yMDIyPC9GZWNoYUV4cGVkaWNpb25GYWN0dXJhQW50ZXJpb3I+CgkJPFNpZ25hdHVyZVZhbHVlRmlybWFGYWN0dXJhQW50ZXJpb3I+RFNJMVhBMUVyQ29xYUVsbisrMjE2ZVRRcE4wcUY3SFFiL0kzUWhGNTRWMTFMRUdZM3VjTVZIMzNHY2VSRTlMdFlKSTdBb0kwWVMzcTBxUE9uK2VUbEg2WkhWWFhrY1ZaeWkyZzwvU2lnbmF0dXJlVmFsdWVGaXJtYUZhY3R1cmFBbnRlcmlvcj4JCSAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAoJPC9FbmNhZGVuYW1pZW50b0ZhY3R1cmFBbnRlcmlvcj4KCTxTb2Z0d2FyZT4KCSAgICA8TGljZW5jaWFUQkFJPlRCQUlQUlVFQkE8L0xpY2VuY2lhVEJBST4KCSAgICA8RW50aWRhZERlc2Fycm9sbGFkb3JhPgogICAgICAgIAk8TklGPkE0ODExOTgyMDwvTklGPgoJICAgIDwvRW50aWRhZERlc2Fycm9sbGFkb3JhPgogICAgICAgICAgICA8Tm9tYnJlPkRGQlRCQUk8L05vbWJyZT4KICAgICAgICAgICAgPFZlcnNpb24+MS4wNC4wMDwvVmVyc2lvbj4KICAgICAgICA8L1NvZnR3YXJlPgogICAgICAgIDxOdW1TZXJpZURpc3Bvc2l0aXZvPkdQNEZDNUo8L051bVNlcmllRGlzcG9zaXRpdm8+CiAgICA8L0h1ZWxsYVRCQUk+CjxkczpTaWduYXR1cmUgeG1sbnM6ZHM9Imh0dHA6Ly93d3cudzMub3JnLzIwMDAvMDkveG1sZHNpZyMiIElkPSJTaWduYXR1cmUtYTNlMDU2ZGYtMjY5NS00MzJkLWIyNDYtMzIxOTRmOWI1Y2M3LVNpZ25hdHVyZSI+PGRzOlNpZ25lZEluZm8+PGRzOkNhbm9uaWNhbGl6YXRpb25NZXRob2QgQWxnb3JpdGhtPSJodHRwOi8vd3d3LnczLm9yZy9UUi8yMDAxL1JFQy14bWwtYzE0bi0yMDAxMDMxNSIvPjxkczpTaWduYXR1cmVNZXRob2QgQWxnb3JpdGhtPSJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNyc2Etc2hhMjU2Ii8+PGRzOlJlZmVyZW5jZSBJZD0iUmVmZXJlbmNlLTAwOGYwM2Y4LWU2ZjYtNDlkMi1iNWYwLWY0M2E5ZjZmZjZjZSIgVHlwZT0iaHR0cDovL3d3dy53My5vcmcvMjAwMC8wOS94bWxkc2lnI09iamVjdCIgVVJJPSIiPjxkczpUcmFuc2Zvcm1zPjxkczpUcmFuc2Zvcm0gQWxnb3JpdGhtPSJodHRwOi8vd3d3LnczLm9yZy9UUi8yMDAxL1JFQy14bWwtYzE0bi0yMDAxMDMxNSIvPjxkczpUcmFuc2Zvcm0gQWxnb3JpdGhtPSJodHRwOi8vd3d3LnczLm9yZy8yMDAwLzA5L3htbGRzaWcjZW52ZWxvcGVkLXNpZ25hdHVyZSIvPjxkczpUcmFuc2Zvcm0gQWxnb3JpdGhtPSJodHRwOi8vd3d3LnczLm9yZy9UUi8xOTk5L1JFQy14cGF0aC0xOTk5MTExNiI+PGRzOlhQYXRoIHhtbG5zOmRzPSJodHRwOi8vd3d3LnczLm9yZy8yMDAwLzA5L3htbGRzaWcjIj5ub3QoYW5jZXN0b3Itb3Itc2VsZjo6ZHM6U2lnbmF0dXJlKTwvZHM6WFBhdGg+PC9kczpUcmFuc2Zvcm0+PC9kczpUcmFuc2Zvcm1zPjxkczpEaWdlc3RNZXRob2QgQWxnb3JpdGhtPSJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGVuYyNzaGE1MTIiLz48ZHM6RGlnZXN0VmFsdWU+bG5QY21PT093d09idUtCd3hqMFFuRXVrKzJKVzFWQXZ5d2EwZXBZMjRUMmVCWTB2VTNSdnpsS0h0d1JEbkVrb1pZZzJZNXhTQU0zb09UM210Ry9kQ0E9PTwvZHM6RGlnZXN0VmFsdWU+PC9kczpSZWZlcmVuY2U+PGRzOlJlZmVyZW5jZSBUeXBlPSJodHRwOi8vdXJpLmV0c2kub3JnLzAxOTAzI1NpZ25lZFByb3BlcnRpZXMiIFVSST0iI1NpZ25hdHVyZS1hM2UwNTZkZi0yNjk1LTQzMmQtYjI0Ni0zMjE5NGY5YjVjYzctU2lnbmVkUHJvcGVydGllcyI+PGRzOkRpZ2VzdE1ldGhvZCBBbGdvcml0aG09Imh0dHA6Ly93d3cudzMub3JnLzIwMDEvMDQveG1sZW5jI3NoYTUxMiIvPjxkczpEaWdlc3RWYWx1ZT5jSEVSUGxFZXB1ODAyL0VBUVVFUkFDejFUaDJvZDFxQUw4dFFCSEwydWJVYStlT0JNYi9UcTBoczE2Qy8zWkFsTDVmNGppUW5KMFNwVXVHY0lVSno2Zz09PC9kczpEaWdlc3RWYWx1ZT48L2RzOlJlZmVyZW5jZT48ZHM6UmVmZXJlbmNlIFVSST0iI1NpZ25hdHVyZS1hM2UwNTZkZi0yNjk1LTQzMmQtYjI0Ni0zMjE5NGY5YjVjYzctS2V5SW5mbyI+PGRzOkRpZ2VzdE1ldGhvZCBBbGdvcml0aG09Imh0dHA6Ly93d3cudzMub3JnLzIwMDEvMDQveG1sZW5jI3NoYTUxMiIvPjxkczpEaWdlc3RWYWx1ZT53R2pnNEJLTCsrUzdpcUk0eWM0ckJTMjVabldmd1k0MVpmZHVCaUN1Q1AxNzljQzRCNVFXNmRnR01hVU0vTmUrSXA3ejI0OXc3R2d4c2EwbUtPQVE5UT09PC9kczpEaWdlc3RWYWx1ZT48L2RzOlJlZmVyZW5jZT48L2RzOlNpZ25lZEluZm8+PGRzOlNpZ25hdHVyZVZhbHVlIElkPSJTaWduYXR1cmUtYTNlMDU2ZGYtMjY5NS00MzJkLWIyNDYtMzIxOTRmOWI1Y2M3LVNpZ25hdHVyZVZhbHVlIj5WWjhyZFBhS2ZXb0NEODNHdHhoVndwc25PNUFEdGtJdFlRTTJ5NG9ndTZGVnZNaEl1aU5xMTBEMExDN3FzdVJtVnU0Y1pZTXh5TjQ1VTBiNDhueDRaKzZvMlMzK0pybXl6cC9NUU5uSTF3L0YwRjc1T3Zab2ZMdWZ0cnN2MUh3TWRHZnJJTnJpdEZGRC9vSGhYL1B5RXdHVmlpYktqMmtUQWlkRnlFV212LytWMWVXcEphcE5Sakt0bkw3YU40bjNyTE5wa1VYNkl0N2lNNWtMb0IxTzM5QndyODVwVmFDOVlrNjNvWEFuS0RaYkVVYW5BdTVWakVTaUt5TGhUM1ZzWmFRaGxoU29OMU1SdGtSa2xPdXhNYUdHeGp0K3h4TzJLTHRjc09tTkd0U0lZc0Y4WjJaN1VhZzNLNTNPeHBtOUE2YWNBQ2VxSjhKaGg3RTlhcDFlREE9PTwvZHM6U2lnbmF0dXJlVmFsdWU+PGRzOktleUluZm8gSWQ9IlNpZ25hdHVyZS1hM2UwNTZkZi0yNjk1LTQzMmQtYjI0Ni0zMjE5NGY5YjVjYzctS2V5SW5mbyI+PGRzOlg1MDlEYXRhPjxkczpYNTA5Q2VydGlmaWNhdGU+TUlJSHNUQ0NCWm1nQXdJQkFnSVFLcDBPYXd4enRtaGI3YVV3VWtYUHFqQU5CZ2txaGtpRzl3MEJBUXNGQURDQmlURUxNQWtHQTFVRUJoTUNSVk14RkRBU0JnTlZCQW9NQzBsYVJVNVFSU0JUTGtFdU1Ub3dPQVlEVlFRTERERkJXbG9nV21sMWNuUmhaMmx5YVNCd2RXSnNhV3R2WVNBdElFTmxjblJwWm1sallXUnZJSEIxWW14cFkyOGdVME5CTVNnd0pnWURWUVFEREI5RFFTQkJRVkJRSUZaaGMyTmhjeUFvTWlrZ0xTQkVSVk5CVWxKUFRFeFBNQjRYRFRFNE1URXhOVEUyTlRZeE5sb1hEVE0zTVRFeU9ESXpNREF3TUZvd2RqRUxNQWtHQTFVRUJoTUNSVk14RHpBTkJnTlZCQW9NQmt4QlRsUkpTekUzTURVR0ExVUVDd3d1UjJGcGJIVWdlbWwxY25SaFoybHlhV0VnTFNCRFpYSjBhV1pwWTJGa2J5QmtaU0JrYVhOd2IzTnBkR2wyYnpFTE1Ba0dBMVVFQ3d3Q1VFTXhFREFPQmdOVkJBTU1CMGRRTkVaRE5Vb3dnZ0VpTUEwR0NTcUdTSWIzRFFFQkFRVUFBNElCRHdBd2dnRUtBb0lCQVFDNWZVS2gzVmR1QUFGaC85WEsvekhrZWhvbW5tT2ZpcHdBaThacGR0TXY0eFdSTUREVzVZd0p1dkEra0NEeHBSYTkwNWlzT1Q2eG5VL3NTREQ4dVlPbjYvUldHMVNXZjB0OGFFcWswa1lmQ3ZRVG9oYjFiVWVjSk5LWld1WjJvcFJ6aEdQekt6cy9SMHlqYmFJSTdSc0Vja3l6bmtEc0ZDc0VqYTZzcU9ZWDk2Vjd1Mm92WlBmTXloRXZBYjBvQnViSU53cHF0UU9lbm1DRjNJVkMwVVhPd1Q0dDVpakdXc2ZSUWVHMTJTck5MZmtncWZld0RFM0lIZERBUkxMQVJCa3hFSzV0aFVCOFJMVlNFaXNrdkx0YXMvL2RWUzNRZ3ZlSktSaDBndk10ek1aRG1RM1BDbWloS05tSWwxcHRZN2krajE2U0VMZGhDNUpRVWhoQzZqU0pBZ01CQUFHamdnTWxNSUlESVRDQnh3WURWUjBTQklHL01JRzhoaFZvZEhSd09pOHZkM2QzTG1sNlpXNXdaUzVqYjIyQkQybHVabTlBYVhwbGJuQmxMbU52YmFTQmtUQ0JqakZITUVVR0ExVUVDZ3crU1ZwRlRsQkZJRk11UVM0Z0xTQkRTVVlnUVRBeE16TTNNall3TFZKTlpYSmpMbFpwZEc5eWFXRXRSMkZ6ZEdWcGVpQlVNVEExTlNCR05qSWdVemd4UXpCQkJnTlZCQWtNT2tGMlpHRWdaR1ZzSUUxbFpHbDBaWEp5WVc1bGJ5QkZkRzl5WW1sa1pXRWdNVFFnTFNBd01UQXhNQ0JXYVhSdmNtbGhMVWRoYzNSbGFYb3dEZ1lEVlIwUEFRSC9CQVFEQWdXZ01CMEdBMVVkRGdRV0JCUmVpQ2MvOHE4QlUzRkdxY0ZLbG43MWlvaHNOVEFmQmdOVkhTTUVHREFXZ0JUQ3ZKZnQyN29RM1N4TStKUEc3MDR6QmtrZjl6Q0NBUjRHQTFVZElBU0NBUlV3Z2dFUk1JSUJEUVlLS3dZQkJBSHpPV1VEQWpDQi9qQWxCZ2dyQmdFRkJRY0NBUllaYUhSMGNEb3ZMM2QzZHk1cGVtVnVjR1V1WTI5dEwyTndjekNCMUFZSUt3WUJCUVVIQWdJd2djY01nY1JDWlhKdFpXVnVJRzExWjJGcklHVjZZV2QxZEhwbGEyOGdkM2QzTG1sNlpXNXdaUzVqYjIwZ1dtbDFjblJoWjJseWFXRnVJR3R2Ym1acFlXNTBlbUVnYVhwaGJpQmhkWEp5WlhScGF5QnJiMjUwY21GMGRXRWdhWEpoYTNWeWNta3VUR2x0YVhSaFkybHZibVZ6SUdSbElHZGhjbUZ1ZEdsaGN5QmxiaUIzZDNjdWFYcGxibkJsTG1OdmJTQkRiMjV6ZFd4MFpTQmxiQ0JqYjI1MGNtRjBieUJoYm5SbGN5QmtaU0JqYjI1bWFXRnlJR1Z1SUdWc0lHTmxjblJwWm1sallXUnZNSUdqQmdnckJnRUZCUWNCQVFTQmxqQ0JrekFsQmdnckJnRUZCUWN3QVlZWmFIUjBjRG92TDI5amMzQmtaWE11YVhwbGJuQmxMbU52YlRCcUJnZ3JCZ0VGQlFjd0FvWmVhSFIwY0RvdkwzZDNkeTVwZW1WdWNHVXVZMjl0TDJOdmJuUmxibWxrYjNNdmFXNW1iM0p0WVdOcGIyNHZZMkZ6WDJsNlpXNXdaUzlsYzE5allYTXZZV1JxZFc1MGIzTXZRVUZRVUU1U1gyTmxjblJmYzJoaE1qVTJMbU55ZERBOUJnTlZIUjhFTmpBME1ES2dNS0F1aGl4b2RIUndPaTh2WTNKc1pHVnpMbWw2Wlc1d1pTNWpiMjB2WTJkcExXSnBiaTlqY214cGJuUmxjbTVoTWpBTkJna3Foa2lHOXcwQkFRc0ZBQU9DQWdFQVlhUEZVMW45UCtUSG5DUzRNOEUveGI1OGhxcGg1QkRneHJGYkJQWVE0RDhhSk9hU0VxZlBQcmhPOWZsWVRGU2NuR2MxaU5kalpialFkQ01vZnpPbFFTM25VeUdEUm9PeG1Jb1F5L1pyT3lISWFpWUZob1lha3M1d1VrdlZSZTQ4WmpIZmUwbGJnWVlwckw3YXFydGs2c0l3YmNmQzAyOTlYVWlOZWNFZEtaYStXY29jbUc5TFhUUTQ5c2RFOGVLRTBIRUtPUmxQVnE1TURYREluNFB5aTBOUGVmajRXZVAzcE95bTVwamlYMkZ4YTFwMHVxL2E3TnhNVEgvVUJBVkczOFZaUzA2TnM1RC9oRHVhdnRpdTJvWUxyc2dCWkJQRHl3MGU4bGJ2ZjN5em1mMytrMGNRWHAwRXRWOFM5VWYxSXdYU3BNUHZNYU16ZWJQUWsrVzY0QWxCVVRuemt0TGRvTjRRNGJURGI3MTJNR092NFAzYjVPVnZFa1RDdW1UZkEzdTV1c0Z0KzN2eTd6bkZnaDc1L0dZb2Zza0I3ckpESkVtNzBob29DQzQyWVkxNXIvYkRzOVRJM3NCUTc3WFcvOEdEelVqRDdPZGQ1cVJCZ2xWMU5QbWx5dnFKSE1Ma0NxSS9leWhJbVBldngwT2lVeEExMFRmaXpkSUxZZDB0RzFzQ3VTSHBKamJPM0FjSVZ5ODl1ekVMQ3hiMFFFTURTWmtwK05aZG1YUWd2MnpVQnFTcGc1U2lIR2VWTHprUjVYYmIzd0RIL3NIV0V6TVVSWXJQTXVTMHovanB5OW5ZQnpRK2JTMTR0K0tJOWZkeWRteXFubzIxTnVpNUNURTlocUJuOWl1V2E2WGhwSUhnWG5hSitzYlIzSHhCZyt1VG9KZkxVaHJGUmFBPTwvZHM6WDUwOUNlcnRpZmljYXRlPjwvZHM6WDUwOURhdGE+PGRzOktleVZhbHVlPjxkczpSU0FLZXlWYWx1ZT48ZHM6TW9kdWx1cz51WDFDb2QxWGJnQUJZZi9WeXY4eDVIb2FKcDVqbjRxY0FJdkdhWGJUTCtNVmtUQXcxdVdNQ2Jyd1BwQWc4YVVXdmRPWXJEaytzWjFQN0Vndy9MbURwK3YwVmh0VWxuOUxmR2hLcE5KR0h3cjBFNklXOVcxSG5DVFNtVnJtZHFLVWM0Umo4eXM3UDBkTW8yMmlDTzBiQkhKTXM1NUE3QlFyQkkydXJLam1GL2VsZTd0cUwyVDN6TW9STHdHOUtBYm15RGNLYXJVRG5wNWdoZHlGUXRGRnpzRStMZVlveGxySDBVSGh0ZGtxelMzNUlLbjNzQXhOeUIzUXdFU3l3RVFaTVJDdWJZVkFmRVMxVWhJckpMeTdXclAvM1ZVdDBJTDNpU2tZZElMekxjekdRNWtOendwb29TalppSmRhYldPNHZvOWVraEMzWVF1U1VGSVlRdW8waVE9PTwvZHM6TW9kdWx1cz48ZHM6RXhwb25lbnQ+QVFBQjwvZHM6RXhwb25lbnQ+PC9kczpSU0FLZXlWYWx1ZT48L2RzOktleVZhbHVlPjwvZHM6S2V5SW5mbz48ZHM6T2JqZWN0Pjx4YWRlczpRdWFsaWZ5aW5nUHJvcGVydGllcyB4bWxuczp4YWRlcz0iaHR0cDovL3VyaS5ldHNpLm9yZy8wMTkwMy92MS4zLjIjIiBJZD0iU2lnbmF0dXJlLWEzZTA1NmRmLTI2OTUtNDMyZC1iMjQ2LTMyMTk0ZjliNWNjNy1RdWFsaWZ5aW5nUHJvcGVydGllcyIgVGFyZ2V0PSIjU2lnbmF0dXJlLWEzZTA1NmRmLTI2OTUtNDMyZC1iMjQ2LTMyMTk0ZjliNWNjNy1TaWduYXR1cmUiIHhtbG5zOmRzPSJodHRwOi8vd3d3LnczLm9yZy8yMDAwLzA5L3htbGRzaWcjIj48eGFkZXM6U2lnbmVkUHJvcGVydGllcyBJZD0iU2lnbmF0dXJlLWEzZTA1NmRmLTI2OTUtNDMyZC1iMjQ2LTMyMTk0ZjliNWNjNy1TaWduZWRQcm9wZXJ0aWVzIj48eGFkZXM6U2lnbmVkU2lnbmF0dXJlUHJvcGVydGllcz48eGFkZXM6U2lnbmluZ1RpbWU+MjAyMC0xMC0wMlQxNDo1OToyMCswMjowMDwveGFkZXM6U2lnbmluZ1RpbWU+PHhhZGVzOlNpZ25pbmdDZXJ0aWZpY2F0ZT48eGFkZXM6Q2VydD48eGFkZXM6Q2VydERpZ2VzdD48ZHM6RGlnZXN0TWV0aG9kIEFsZ29yaXRobT0iaHR0cDovL3d3dy53My5vcmcvMjAwMS8wNC94bWxlbmMjc2hhNTEyIi8+PGRzOkRpZ2VzdFZhbHVlPitpSm9sbElmMTFEK0E5L21Bek9VTTZTU0p2RlBPbmVvT243TklLZitOcWtwY0U3VlVNeDR4aUdqdzBEOEpOcmZyZXhKd3htbE1UV2QzRWcvZDNCcTJRPT08L2RzOkRpZ2VzdFZhbHVlPjwveGFkZXM6Q2VydERpZ2VzdD48eGFkZXM6SXNzdWVyU2VyaWFsPjxkczpYNTA5SXNzdWVyTmFtZT5DTj1DQSBBQVBQIFZhc2NhcyAoMikgLSBERVNBUlJPTExPLCBPVT1BWlogWml1cnRhZ2lyaSBwdWJsaWtvYSAtIENlcnRpZmljYWRvIHB1YmxpY28gU0NBLCBPPUlaRU5QRSBTLkEuLCBDPUVTPC9kczpYNTA5SXNzdWVyTmFtZT48ZHM6WDUwOVNlcmlhbE51bWJlcj41NjY0MzA1ODg2NDc1Nzk4MjczMjIwNjQ2MzYwMTA4Mjc0ODg0MjwvZHM6WDUwOVNlcmlhbE51bWJlcj48L3hhZGVzOklzc3VlclNlcmlhbD48L3hhZGVzOkNlcnQ+PC94YWRlczpTaWduaW5nQ2VydGlmaWNhdGU+PHhhZGVzOlNpZ25hdHVyZVBvbGljeUlkZW50aWZpZXI+PHhhZGVzOlNpZ25hdHVyZVBvbGljeUlkPjx4YWRlczpTaWdQb2xpY3lJZD48eGFkZXM6SWRlbnRpZmllcj5odHRwczovL3d3dy5iYXR1ei5ldXMvZml0eGF0ZWdpYWsvYmF0dXovdGlja2V0YmFpL3NpbmFkdXJhX2VsZWt0cm9uaWtvYXJlbl96ZWhhenRhcGVuYWtfZXNwZWNpZmljYWNpb25lc19kZV9sYV9maXJtYV9lbGVjdHJvbmljYV92MV8wLnBkZjwveGFkZXM6SWRlbnRpZmllcj48eGFkZXM6RGVzY3JpcHRpb24vPjwveGFkZXM6U2lnUG9saWN5SWQ+PHhhZGVzOlNpZ1BvbGljeUhhc2g+PGRzOkRpZ2VzdE1ldGhvZCBBbGdvcml0aG09Imh0dHA6Ly93d3cudzMub3JnLzIwMDEvMDQveG1sZW5jI3NoYTI1NiIvPjxkczpEaWdlc3RWYWx1ZT5RdXpuOTh4M1BNYlNId2JVemFqNWY1S09waUgwdThidm13YmJiTmtPOUVzPTwvZHM6RGlnZXN0VmFsdWU+PC94YWRlczpTaWdQb2xpY3lIYXNoPjx4YWRlczpTaWdQb2xpY3lRdWFsaWZpZXJzPjx4YWRlczpTaWdQb2xpY3lRdWFsaWZpZXI+PHhhZGVzOlNQVVJJPmh0dHBzOi8vd3d3LmJhdHV6LmV1cy9maXR4YXRlZ2lhay9iYXR1ei90aWNrZXRiYWkvc2luYWR1cmFfZWxla3Ryb25pa29hcmVuX3plaGF6dGFwZW5ha19lc3BlY2lmaWNhY2lvbmVzX2RlX2xhX2Zpcm1hX2VsZWN0cm9uaWNhX3YxXzAucGRmPC94YWRlczpTUFVSST48L3hhZGVzOlNpZ1BvbGljeVF1YWxpZmllcj48L3hhZGVzOlNpZ1BvbGljeVF1YWxpZmllcnM+PC94YWRlczpTaWduYXR1cmVQb2xpY3lJZD48L3hhZGVzOlNpZ25hdHVyZVBvbGljeUlkZW50aWZpZXI+PC94YWRlczpTaWduZWRTaWduYXR1cmVQcm9wZXJ0aWVzPjx4YWRlczpTaWduZWREYXRhT2JqZWN0UHJvcGVydGllcz48eGFkZXM6RGF0YU9iamVjdEZvcm1hdCBPYmplY3RSZWZlcmVuY2U9IiNSZWZlcmVuY2UtMDA4ZjAzZjgtZTZmNi00OWQyLWI1ZjAtZjQzYTlmNmZmNmNlIj48eGFkZXM6RGVzY3JpcHRpb24vPjx4YWRlczpPYmplY3RJZGVudGlmaWVyPjx4YWRlczpJZGVudGlmaWVyIFF1YWxpZmllcj0iT0lEQXNVUk4iPnVybjpvaWQ6MS4yLjg0MC4xMDAwMy41LjEwOS4xMDwveGFkZXM6SWRlbnRpZmllcj48eGFkZXM6RGVzY3JpcHRpb24vPjwveGFkZXM6T2JqZWN0SWRlbnRpZmllcj48eGFkZXM6TWltZVR5cGU+dGV4dC94bWw8L3hhZGVzOk1pbWVUeXBlPjx4YWRlczpFbmNvZGluZy8+PC94YWRlczpEYXRhT2JqZWN0Rm9ybWF0PjwveGFkZXM6U2lnbmVkRGF0YU9iamVjdFByb3BlcnRpZXM+PC94YWRlczpTaWduZWRQcm9wZXJ0aWVzPjwveGFkZXM6UXVhbGlmeWluZ1Byb3BlcnRpZXM+PC9kczpPYmplY3Q+PC9kczpTaWduYXR1cmU+PC9UOlRpY2tldEJhaT4="
                    }
                }
            };
        }
    }
}
