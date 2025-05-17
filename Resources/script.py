from PIL import Image, ImageOps

def invert_image_color(image_path, output_path):
    try:
        # Open the image
        image = Image.open(image_path)

        # Convert to RGB if not already
        if image.mode != 'RGB':
            image = image.convert('RGB')

        # Invert the image
        inverted_image = ImageOps.invert(image)

        # Save the result
        inverted_image.save(output_path)

        print(f"Inverted image saved to: {output_path}")
    except Exception as e:
        print(f"Error: {e}")

# Example usage
input_path = 'SilksongLogo.png'       # Replace with your input image file
output_path = 'SilksongLogoNew.png'  # Desired output file
invert_image_color(input_path, output_path)

