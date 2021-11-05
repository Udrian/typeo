import argparse, os
import zipfile
from os.path import basename
from os.path import isfile, isdir
 
def main():
    parser = argparse.ArgumentParser()
    parser.add_argument('-p', '--project', type=str, required=True, help="Project name to build")
    parser.add_argument('-b', '--build', type=int, required=True, help="Build number to append to versioning")
    parser.add_argument('-o', '--output', type=str, default="..", help="Output folder")
    args = parser.parse_args()

    pack(args.project, [args.project], args.build, [], args.output)

def pack(package, projects, build_number, externals=[], output_prefix=".."):
    version = getVersion(projects[0], build_number)
    package_name = basename(package)
    output = "{}/bin/package/{}".format(output_prefix, package_name)
    print("Packing '{}' '{}' to output '{}'".format(package_name, version, output))

    zipfilename = "{}/{}-v{}.zip".format(output, package_name, version)

    os.makedirs(output, exist_ok=True)
    with zipfile.ZipFile(zipfilename, 'w') as zipObj:
        for project in projects:
            project_name = basename(project)
            print("Adding project '{}' to zip".format(project))
            path = "{}/bin/builds/{}/Release".format(output_prefix, project)
            
            addFileToZip(zipObj, "{}/{}.runtimeconfig.json".format(path, project_name), "")
            addFileToZip(zipObj, "{}/{}.deps.json"         .format(path, project_name), "")
            addFileToZip(zipObj, "{}/{}.dll"               .format(path, project_name), "")
            addFileToZip(zipObj, "{}/{}.exe"               .format(path, project_name), "")

            addExternal(externals, zipObj, path)
        addExternal(externals, zipObj, output_prefix)
        
        #Add readme and releasenotes
        addFileToZip(zipObj, "./../Readme-TypeO.txt", "")
        addFileToZip(zipObj, "./../{}/ReleaseNotes-{}.txt".format(package, package_name), "")
    return zipfilename

def addExternal(externals, zipObj, path):
    for external in externals:
        externalPath = "{}/{}".format(path, external)
        if isdir(externalPath):
            print("Checking project external '{}' to zip".format(external))
        
        if isfile(externalPath):
            addFileToZip(zipObj, "{}".format(externalPath), "")
        
        if isdir(externalPath):
            for filename in os.listdir(externalPath):
                addFileToZip(zipObj, "{}/{}".format(externalPath, filename), "")

def addFileToZip(zipObj, filepath, pathTo):
    if isfile(filepath):
        filename = basename(filepath)
        filepathTo = "./{}/{}".format(pathTo, filename)
        print("... Adding file: {}".format(filepathTo))
        zipObj.write(filepath, filepathTo, zipfile.ZIP_DEFLATED)

def getVersion(project, build_number):
    path = "../{0}/version".format(project)

    with open(path) as f:
        return "{}.{}".format(f.readline().replace("\n", "").replace("\r", "").replace("v", ""), build_number)

if __name__ == "__main__":
    main()