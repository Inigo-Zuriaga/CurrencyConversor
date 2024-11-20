import { Injectable } from '@angular/core';
import jsPDF from 'jspdf';
import html2canvas from 'html2canvas';
@Injectable({
  providedIn: 'root'
})
export class PdfService {

  constructor() { }

  // generatePdf() {
  //   const element = document.getElementById('content'); // ID del elemento HTML
  //   if (element) {
  //     html2canvas(element).then((canvas) => {
  //       const imgData = canvas.toDataURL('image/png');
  //       const pdf = new jsPDF('p', 'mm', 'a4'); // Formato A4 en mm
  //       const imgWidth = 210; // Ancho de página A4
  //       const imgHeight = (canvas.height * imgWidth) / canvas.width; // Escalar alto proporcionalmente
  //
  //       pdf.addImage(imgData, 'PNG', 0, 0, imgWidth, imgHeight);
  //       pdf.save('documento.pdf'); // Nombre del archivo
  //     });
  //   }
  // }

  // generatePdf(elementId: string, fileName: string = 'document.pdf') {
  //
  //   const element = document.getElementById(elementId);
  //   if (element) {
  //     element.style.display = 'block';
  //     html2canvas(element,{scale:2}).then((canvas) => {
  //       const imgData = canvas.toDataURL('image/png');
  //       const pdf = new jsPDF('p', 'mm', 'a4'); // Formato A4
  //       const pdfWidth = 210; // Ancho de página en mm (A4)
  //       const imgWidth = canvas.width;
  //       const imgHeight = canvas.height;
  //
  //
  //       // Escalar el contenido al ancho del PDF
  //       const ratio = imgHeight / imgWidth; // Proporción
  //       const scaledWidth = pdfWidth; // Ancho ajustado al PDF
  //       const scaledHeight = scaledWidth * ratio; // Altura ajustada proporcionalmente
  //
  //
  //       const logoUrl = 'assets/images/OIP.png'; // Ruta relativa desde la carpeta 'src'
  //
  //       // Agregar la imagen (logo) desde la carpeta 'assets'
  //       pdf.addImage(logoUrl, 'PNG',10, 10, 50, 20);
  //
  //       pdf.addImage(imgData, 'PNG', 0, 10, scaledWidth, scaledHeight); // Insertar imagen
  //
  //       pdf.setFontSize(20);
  //       pdf.text('Informe Completo de la Transacción', 10, 10);
  //
  //       pdf.save(fileName); // Descargar el archivo
  //     });
  //   } else {
  //     console.error(`El elemento con ID "${elementId}" no existe.`);
  //   }
  // }

  async generatePdf(elementId: string, fileName: string = 'document.pdf') {
    const element = document.getElementById(elementId);
    if (element) {
      const buttons = element.querySelectorAll('button');
      buttons.forEach((button) => button.remove()); // Eliminar botones
      // Muestra temporalmente el elemento si está oculto
      const originalDisplay = element.style.display;
      element.style.display = 'block';

      try {
        // Carga el logo como base64
        // const logoBase64 = await this.loadImageAsBase64('assets/images/OIP.png');
        const logoBase64 = await this.loadImageAsBase64('assets/icon.png');

        // Renderiza el contenido HTML como canvas
        const canvas = await html2canvas(element, { scale: 2 });
        const imgData = canvas.toDataURL('image/png');

        // Configuración del PDF
        const pdf = new jsPDF('p', 'mm', 'a4');
        const pdfWidth = 210; // Ancho en mm (A4)
        const imgWidth = canvas.width;
        const imgHeight = canvas.height;
        const ratio = imgHeight / imgWidth; // Proporción
        const scaledWidth = pdfWidth; // Ajustar al ancho del PDF
        const scaledHeight = scaledWidth * ratio; // Altura ajustada proporcionalmente

        // Agrega el logo al PDF
        if (logoBase64) {
          pdf.addImage(logoBase64, 'PNG', 5, -10, 45, 45); // Ajusta la posición y el tamaño
        }

        // Agrega el contenido HTML (tabla) al PDF
        pdf.addImage(imgData, 'PNG', 0, 40, scaledWidth, scaledHeight);

        // Agrega texto adicional al PDF
        pdf.setFontSize(20);
        pdf.text('Informe Completo de la Transacción', 10, 35);

        // Descarga el archivo PDF
        pdf.save(fileName);
        window.location.reload();
      } catch (error) {
        console.error('Error al generar el PDF:', error);
      } finally {
        // Restaura el estilo original del elemento
        element.style.display = originalDisplay;
      }
    } else {
      console.error(`El elemento con ID "${elementId}" no existe.`);
    }
  }

  // Carga una imagen y la convierte a Base64
  private loadImageAsBase64(imagePath: string): Promise<string | null> {
    return new Promise((resolve, reject) => {
      const img = new Image();
      img.crossOrigin = 'Anonymous'; // Evita problemas de CORS
      img.onload = () => {
        const canvas = document.createElement('canvas');
        canvas.width = img.width;
        canvas.height = img.height;
        const ctx = canvas.getContext('2d');
        if (ctx) {
          ctx.drawImage(img, 0, 0);
          resolve(canvas.toDataURL('image/png'));
        } else {
          reject('No se pudo obtener el contexto del canvas.');
        }
      };
      img.onerror = () => reject('Error al cargar la imagen.');
      img.src = imagePath;
    });
  }




}
