package cse;

import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.PrintStream;

import javax.xml.parsers.DocumentBuilder;
import javax.xml.parsers.DocumentBuilderFactory;
import javax.xml.parsers.ParserConfigurationException;

import org.w3c.dom.Document;
import org.w3c.dom.Element;
import org.w3c.dom.NodeList;
import org.xml.sax.SAXException;

public class Compute {
    public static void main(String[] args) throws NumberFormatException, IOException {
        DocumentBuilderFactory factory = DocumentBuilderFactory.newInstance();
        factory.setIgnoringComments(true);
        factory.setIgnoringElementContentWhitespace(true);

        // Use the factory object to return a parser-specific instance
        DocumentBuilder parser = null;
        try {
            parser = factory.newDocumentBuilder();
        } catch (ParserConfigurationException e) {
            e.printStackTrace();
        }

        // Use one of the five parse() methods of DocumentBuilder to
        // read the XML document and return an org.w3c.dom.Document object.
        Document spec = null;
        try {
            spec = parser.parse(new FileInputStream(args[0]));
        } catch (FileNotFoundException e) {
            e.printStackTrace();
        } catch (SAXException e) {
            e.printStackTrace();
        } catch (IOException e) {
            e.printStackTrace();
        }
        
        ARMAChromosome arma = new ARMAChromosome();
        
        Element root = spec.getDocumentElement();
        arma.setP(Integer.valueOf(root.getAttribute("p")));
        arma.setQ(Integer.valueOf(root.getAttribute("q")));
        
        Element arElem = (Element) root.getElementsByTagName("AR").item(0);
        NodeList arCoefs = arElem.getElementsByTagName("coefficient");
        for (int i = 0; i < arCoefs.getLength(); i++) {
            Element coef = (Element) arCoefs.item(i);
            arma.setCoeff(Integer.valueOf(coef.getAttribute("lag")) - 1,
                    Double.valueOf(coef.getAttribute("value")));
        }
        
        arElem = (Element) root.getElementsByTagName("MA").item(0);
        arCoefs = arElem.getElementsByTagName("coefficient");
        for (int i = 0; i < arCoefs.getLength(); i++) {
            Element coef = (Element) arCoefs.item(i);
            coef.getAttribute("lag");
            coef.getAttribute("value");
            arma.setCoeff(arma.getP() + Integer.valueOf(coef.getAttribute("lag")) - 1,
                    Double.valueOf(coef.getAttribute("value")));
        }
        
        System.out.println("Data set: " + args[1]);
        DataSet ds = new DataSet(args[1]);
        double[] ts = ds.loadData(1);
        
        PrintStream ps = new PrintStream(new FileOutputStream(args[2]));
        arma.calcRmseForTestSet(ts, ps);
        //System.out.println("RMSEtest = " + arma.calcRmseForTestSet(ts));
        ps.close();
    }
}
