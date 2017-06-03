function httpGet(theUrl)
{
    var xmlHttp = new XMLHttpRequest();
    xmlHttp.open( "POST", theUrl, false );
    xmlHttp.send( null );
    return xmlHttp.responseText;
}


function xmlToJson(xml) {

    var obj = {};

    if (xml.nodeType == 1) { // element
        if (xml.attributes.length > 0) {
            obj["@attributes"] = {};
            for (var j = 0; j < xml.attributes.length; j++) {
                var attribute = xml.attributes.item(j);
                obj["@attributes"][attribute.nodeName] = attribute.nodeValue;
            }
        }
    } else if (xml.nodeType == 3) { // text
        obj = xml.nodeValue;
    }

    if (xml.hasChildNodes() && xml.childNodes.length === 1 && xml.childNodes[0].nodeType === 3) {
        obj = xml.childNodes[0].nodeValue;
    }
    else if (xml.hasChildNodes()) {
        for(var i = 0; i < xml.childNodes.length; i++) {
            var item = xml.childNodes.item(i);
            var nodeName = item.nodeName;
            if (typeof(obj[nodeName]) == "undefined") {
                obj[nodeName] = xmlToJson(item);
            } else {
                if (typeof(obj[nodeName].push) == "undefined") {
                    var old = obj[nodeName];
                    obj[nodeName] = [];
                    obj[nodeName].push(old);
                }
                obj[nodeName].push(xmlToJson(item));
            }
        }
    }
    return obj;
}

var xml= httpGet('/WebPageService/Service.asmx/getObject');
var xmlDOM = new DOMParser().parseFromString(xml, 'text/xml');

var json= xmlToJson(xmlDOM);
console.log("json",json);
var projectItem="";
for( i=0;i<json.ArrayOfResponce.Responce.length;i++){
// for( i=0;i<3;i++){
     projectItem +=
        ' <div class=" post-content col-md-4 ">' +
            ' <div class=" project-bg ">' +
                ' <div class="post-image image-content"  ><img src="json.ArrayOfResponce.Responce[i]._url">json.ArrayOfResponce.Responce[i]._url</div>' +
                '<h4 class=" header--name">'+json.ArrayOfResponce.Responce[i]._name+'</h4>' +
                '<div class=" detail--content"> <span style="margin-left: 25px"></span>'+json.ArrayOfResponce.Responce[i]._description+'</div>' +
                '<div class="some-height"></div>'+
                '<div class=" read--more_btn--content"  > <a onclick="OnClick(\''+ json.ArrayOfResponce.Responce[i]._name + '\')" title="Read more" class="post-button read--more_btn"><i class="fa fa-chevron-circle-right"></i>Read Detail</a> </div>'+
            '</div>'+
        '</div>'
    var ProjectContent = document.getElementById('project_item');
}
ProjectContent.innerHTML = projectItem;
function OnClick(val){

    console.log("click",val);
    window.ProjectName = val;
    window.location.href = "../main/main.html";
}