using FastReport;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;


namespace Rift.Models
{
    public class Relatorios
    {
        public static byte[] GerarRelatorio<T>(string pathRelatorio, List<T> fonteDeDados, string nomeDaFonteDeDados, TiposDeRelatorios tipoDeRelatorio, Dictionary<string, string> parametros)
        {
            byte[] relatorioGerado = null;

            MemoryStream arquivoEmMemoria = new MemoryStream(File.ReadAllBytes(pathRelatorio));
            relatorioGerado = GerarRelatorio<T>(arquivoEmMemoria, fonteDeDados, nomeDaFonteDeDados, null, string.Empty, tipoDeRelatorio, parametros);

            return relatorioGerado;
        }

        public static byte[] GerarRelatorio<T>(string pathRelatorio, List<T> fonteDeDados, string nomeDaFonteDeDados, IList dadosCustomizados, string nomeDosDadosCustomizados, TiposDeRelatorios tipoDeRelatorio, Dictionary<string, string> parametros)
        {
            byte[] relatorioGerado = null;

            MemoryStream arquivoEmMemoria = new MemoryStream(File.ReadAllBytes(pathRelatorio));
            relatorioGerado = GerarRelatorio<T>(arquivoEmMemoria, fonteDeDados, nomeDaFonteDeDados, dadosCustomizados, nomeDosDadosCustomizados, tipoDeRelatorio, parametros);

            return relatorioGerado;
        }

        public static byte[] GerarRelatorio<T>(MemoryStream arquivoEmMemoria, List<T> fonteDeDados, string nomeDaFonteDeDados, TiposDeRelatorios tipoDeRelatorio, Dictionary<string, string> parametros)
        {
            byte[] relatorioGerado = null;
            relatorioGerado = GerarRelatorio<T>(arquivoEmMemoria, fonteDeDados, nomeDaFonteDeDados, null, string.Empty, tipoDeRelatorio, parametros);

            return relatorioGerado;
        }

        public static byte[] GerarRelatorio<T>(MemoryStream modeloDeRelatorio, List<T> fonteDeDados, string nomeDaFonteDeDados, IList dadosCustomizados, string nomeDosDadosCustomizados, TiposDeRelatorios tipoDeRelatorio, Dictionary<string, string> parametros)
        {
            byte[] relatorioGerado = null;

            FastReport.Utils.Config.WebMode = true;
            using (Report relatorio = new Report())
            {
                relatorio.Load(modeloDeRelatorio);

                relatorio.RegisterData(fonteDeDados, nomeDaFonteDeDados);

                if (!string.IsNullOrEmpty(nomeDosDadosCustomizados) && dadosCustomizados != null)
                {
                    relatorio.RegisterData(dadosCustomizados, nomeDosDadosCustomizados);
                }

                if (parametros != null)
                {
                    foreach (var parametro in parametros)
                    {
                        var param = relatorio.Parameters.FindByName(parametro.Key);
                        if (param != null) param.Value = parametro.Value;
                    }
                }


                relatorio.SetParameterValue("DataHora", DateTime.Now);
                if (relatorio.Prepare())
                {
                    relatorioGerado = ExportarRelatorio(relatorio, tipoDeRelatorio);
                }
            }

            return relatorioGerado;
        }

        public static byte[] AdicionarFonteDeDados(string pathRelatorio, IList dadosCustomizados, string nomeDosDadosCustomizados)
        {
            Report report = new Report();

            report.Load(pathRelatorio);
            report.RegisterData(dadosCustomizados, nomeDosDadosCustomizados, 10);
            report.GetDataSource(nomeDosDadosCustomizados).Enabled = true;
            report.Save(pathRelatorio);
            report.Dispose();

            return File.ReadAllBytes(pathRelatorio);
        }

        static byte[] ExportarRelatorio(Report report, TiposDeRelatorios tipoDeRelatorio)
        {
            FastReport.Export.ExportBase relatorio = new FastReport.Export.ExportBase();

            byte[] relatorioGerado = null;

            switch (tipoDeRelatorio)
            {
                case TiposDeRelatorios.CSV:
                    relatorio = new FastReport.Export.Csv.CSVExport();
                    break;
                case TiposDeRelatorios.HTML:
                    relatorio = new FastReport.Export.Html.HTMLExport();
                    break;
                case TiposDeRelatorios.PDF:
                    relatorio = new FastReport.Export.Pdf.PDFExport();
                    break;
                case TiposDeRelatorios.RTF:
                    relatorio = new FastReport.Export.RichText.RTFExport();
                    break;
                case TiposDeRelatorios.TXT:
                    relatorio = new FastReport.Export.Text.TextExport();
                    break;
                case TiposDeRelatorios.XML:
                    relatorio = new FastReport.Export.Xml.XMLExport();
                    break;
            }

            using (MemoryStream relatorioExportadoEmMemoria = new MemoryStream())
            {
                report.Report.Export(relatorio, relatorioExportadoEmMemoria);
                relatorioGerado = relatorioExportadoEmMemoria.ToArray();
            }

            relatorio.Dispose();

            return relatorioGerado;
        }
    }
}