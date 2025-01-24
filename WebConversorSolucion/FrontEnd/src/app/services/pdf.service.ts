import { Injectable } from '@angular/core';
import jsPDF from 'jspdf';
import html2canvas from 'html2canvas';

@Injectable({
  providedIn: 'root'
})
export class PdfService {

  constructor() { }


  // generatePdfFromHtml(htmlContent: string, fileName: string = 'document.pdf'): void {
  //   const pdf = new jsPDF('p', 'mm', 'a4');
  //
  //   pdf.html(htmlContent, {
  //     callback: (doc) => {
  //       doc.save(fileName); // Descargar el PDF
  //     },
  //     x: 10, // Margen X
  //     y: 10, // Margen Y
  //     width: 400 // Ancho del contenido
  //   });
  // }

  // async generatePdfFromElement(element: HTMLElement, fileName: string = 'document.pdf') {
  //   try {
  //     const buttons = element.querySelectorAll('button');
  //     buttons.forEach((button) => button.remove()); // Eliminar botones si no quieres que aparezcan en el PDF
  //
  //     // Renderizar el contenido HTML como canvas
  //     const canvas = await html2canvas(element, { scale: 2 });
  //     const imgData = canvas.toDataURL('image/png');
  //
  //     // Crear el PDF
  //     const pdf = new jsPDF('p', 'mm', 'a4');
  //     const pdfWidth = 210; // Ancho de página A4 en mm
  //     const imgWidth = canvas.width;
  //     const imgHeight = canvas.height;
  //     const ratio = imgHeight / imgWidth; // Proporción de la imagen
  //     const scaledWidth = pdfWidth; // Ajustar al ancho del PDF
  //     const scaledHeight = scaledWidth * ratio; // Altura ajustada proporcionalmente
  //
  //     // Agregar la imagen del contenido renderizado al PDF
  //     pdf.addImage(imgData, 'PNG', 0, 10, scaledWidth, scaledHeight);
  //
  //     // Agregar texto adicional al PDF si es necesario
  //     pdf.setFontSize(20);
  //     pdf.text('Informe Completo de la Transacción', 10, 10);
  //
  //     // Descargar el archivo PDF
  //     pdf.save(fileName);
  //   } catch (error) {
  //     console.error('Error al generar el PDF:', error);
  //   }
  // }


  // async generatePdf(elementId: string, fileName: string = 'document.pdf') {
  //   const element = document.getElementById(elementId);
  //   if (element) {
  //     const buttons = element.querySelectorAll('button');
  //     buttons.forEach((button) => button.remove()); // Eliminar botones
  //     // Muestra temporalmente el elemento si está oculto
  //     const originalDisplay = element.style.display;
  //     element.style.display = 'block';
  //
  //     try {
  //       // Carga el logo como base64
  //       // const logoBase64 = await this.loadImageAsBase64('assets/images/OIP.png');
  //       const logoBase64 = await this.loadImageAsBase64('assets/icon.png');
  //
  //       // Renderiza el contenido HTML como canvas
  //       const canvas = await html2canvas(element, { scale: 2 });
  //       const imgData = canvas.toDataURL('image/png');
  //
  //       // Configuración del PDF
  //       const pdf = new jsPDF('p', 'mm', 'a4');
  //       const pdfWidth = 210; // Ancho en mm (A4)
  //       const imgWidth = canvas.width;
  //       const imgHeight = canvas.height;
  //       const ratio = imgHeight / imgWidth; // Proporción
  //       const scaledWidth = pdfWidth; // Ajustar al ancho del PDF
  //       const scaledHeight = scaledWidth * ratio; // Altura ajustada proporcionalmente
  //
  //       // Agrega el logo al PDF
  //       if (logoBase64) {
  //         pdf.addImage(logoBase64, 'PNG', 5, -10, 45, 45); // Ajusta la posición y el tamaño
  //       }
  //
  //       // Agrega el contenido HTML (tabla) al PDF
  //       pdf.addImage(imgData, 'PNG', 0, 40, scaledWidth, scaledHeight);
  //
  //       // Agrega texto adicional al PDF
  //       pdf.setFontSize(20);
  //       pdf.text('Informe Completo de la Transacción', 10, 35);
  //
  //       // Descarga el archivo PDF
  //       pdf.save(fileName);
  //       window.location.reload();
  //     } catch (error) {
  //       console.error('Error al generar el PDF:', error);
  //     } finally {
  //       // Restaura el estilo original del elemento
  //       element.style.display = originalDisplay;
  //     }
  //   } else {
  //     console.error(`El elemento con ID "${elementId}" no existe.`);
  //   }
  // }
  //
  // // Carga una imagen y la convierte a Base64
  // private loadImageAsBase64(imagePath: string): Promise<string | null> {
  //   return new Promise((resolve, reject) => {
  //     const img = new Image();
  //     img.crossOrigin = 'Anonymous'; // Evita problemas de CORS
  //     img.onload = () => {
  //       const canvas = document.createElement('canvas');
  //       canvas.width = img.width;
  //       canvas.height = img.height;
  //       const ctx = canvas.getContext('2d');
  //       if (ctx) {
  //         ctx.drawImage(img, 0, 0);
  //         resolve(canvas.toDataURL('image/png'));
  //       } else {
  //         reject('No se pudo obtener el contexto del canvas.');
  //       }
  //     };
  //     img.onerror = () => reject('Error al cargar la imagen.');
  //     img.src = imagePath;
  //   });
  // }

}
