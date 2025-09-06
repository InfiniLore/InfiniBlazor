import json
import os
import re
import subprocess
import tempfile
import xml.etree.ElementTree as ET
from typing import List, Dict, Optional

# ----------------------------------------------------------------------------------------------------------------------
# CONFIGURATION - Top level constants you can modify
# ----------------------------------------------------------------------------------------------------------------------
DIRECTORY_PACKAGES_PROPS_PATH = "../Directory.Packages.props"
OUTPUT_JSON_FILE = "../src/InfiniLore.InfiniBlazor/wwwroot/libs/emotes/emotes_lucide.json"
LUCIDE_PACKAGE_NAME = "lucide-static"
INFINILORE_LUCIDE_PACKAGE_PATTERN = "InfiniLore.Lucide"

# ----------------------------------------------------------------------------------------------------------------------
# FUNCTIONS
# ----------------------------------------------------------------------------------------------------------------------
def extract_lucide_version_from_props(props_file_path: str) -> Optional[str]:
    try:
        if not os.path.exists(props_file_path):
            print(f"Directory.Packages.props not found at: {props_file_path}")
            return None

        tree = ET.parse(props_file_path)
        root = tree.getroot()

        for item in root.findall(".//PackageVersion"):
            include = item.get("Include", "")
            if INFINILORE_LUCIDE_PACKAGE_PATTERN in include:
                version = item.get("Version", "")
                print(f"Found {include} version: {version}")

                # Format: 0.33.541 -> we want 541
                match = re.search(r'\.(\d+)$', version)
                if match:
                    lucide_version = match.group(1)
                    print(f"Extracted Lucide version: {lucide_version}")
                    return lucide_version

        print(f"No {INFINILORE_LUCIDE_PACKAGE_PATTERN} package found in {props_file_path}")
        return None

    except Exception as e:
        print(f"Error parsing {props_file_path}: {e}")
        return None

def extract_lucide_icons_from_npm(lucide_version: Optional[str] = None) -> List[str]:
    # Create a temporary directory
    with tempfile.TemporaryDirectory() as temp_dir:
        print(f"Working in temporary directory: {temp_dir}")

        original_cwd = os.getcwd()
        os.chdir(temp_dir)

        try:
            print(f"Installing {LUCIDE_PACKAGE_NAME} package...")
            subprocess.run(["npm", "init", "-y"], check=True, capture_output=True)

            package_to_install = LUCIDE_PACKAGE_NAME
            if lucide_version:
                print(f"Attempting to install version matching Lucide {lucide_version}")

            subprocess.run(["npm", "install", package_to_install], check=True, capture_output=True)

            # Find all SVG files in the icons directory
            possible_icon_dirs = [
                os.path.join("node_modules", LUCIDE_PACKAGE_NAME, "icons"),
                os.path.join("node_modules", LUCIDE_PACKAGE_NAME, "src"),
                os.path.join("node_modules", LUCIDE_PACKAGE_NAME)
            ]

            icons_dir = None
            for potential_dir in possible_icon_dirs:
                if os.path.exists(potential_dir):
                    svg_files = [f for f in os.listdir(potential_dir) if f.endswith('.svg')]
                    if svg_files:
                        icons_dir = potential_dir
                        print(f"Found icons directory: {icons_dir} with {len(svg_files)} SVG files")
                        break

            if not icons_dir:
                print("Could not find icons directory with SVG files")
                return []

            # Extract icon names from SVG filenames
            icon_names = []
            for filename in os.listdir(icons_dir):
                if filename.endswith('.svg'):
                    icon_name = filename[:-4]
                    icon_names.append(icon_name)

            return sorted(icon_names)

        except subprocess.CalledProcessError as e:
            print(f"Error running npm command: {e}")
            return []
        finally:
            os.chdir(original_cwd)

def generate_icon_entries(icon_names: List[str]) -> List[Dict]:
    entries = []

    for icon_name in icon_names:
        keys = [f"li{icon_name}", f"li-{icon_name}"]

        entry = {
            "keys": keys,
            "data": icon_name,
            "contentType": "LucideIconName"
        }
        entries.append(entry)

    return entries

def main():
    print("=" * 80)
    print("Lucide Icons JSON Generator")
    print("=" * 80)

    # Extract version from Directory.Packages.props
    print(f"1. Extracting Lucide version from {DIRECTORY_PACKAGES_PROPS_PATH}...")
    lucide_version = extract_lucide_version_from_props(DIRECTORY_PACKAGES_PROPS_PATH)

    if lucide_version:
        print(f"   Found Lucide version: {lucide_version}")
    else:
        print( "   Could not extract version, proceeding with latest")

    # Try npm approach first
    print("\n2. Attempting to extract icons from npm package...")
    icon_names = extract_lucide_icons_from_npm(lucide_version)

    if not icon_names:
        print("   Failed to extract icons from both npm and GitHub")
        return

    print(f"   Successfully extracted {len(icon_names)} icons")

    # Generate JSON entries
    print("\n3. Generating JSON entries...")
    entries = generate_icon_entries(icon_names)

    # Save to file
    print(f"4. Saving to {OUTPUT_JSON_FILE}...")
    try:
        with open(OUTPUT_JSON_FILE, 'w', encoding='utf-8') as f:
            json.dump(entries, f, indent=2, ensure_ascii=False)

        print(f"   Generated {len(entries)} entries saved to {OUTPUT_JSON_FILE}")

        # Show statistics
        print(f"\n" + "=" * 80)
        print("SUMMARY")
        print("=" * 80)
        print(f"Lucide version targeted: {lucide_version or 'latest'}")
        print(f"Total icons processed: {len(icon_names)}")
        print(f"Output file: {OUTPUT_JSON_FILE}")

        # Show first few examples
        print(f"\nFirst 3 examples:")
        for entry in entries[:3]:
            print(f"  - Keys: {entry['keys']}")
            print(f"    Data: {entry['data']}")
            print()

        # Show some sample icon names
        print(f"Sample icon names: {', '.join(icon_names[:10])}...")

    except Exception as e:
        print(f"   ✗ Error saving file: {e}")

if __name__ == "__main__":
    main()